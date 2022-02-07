using UnityEngine;
 using System;
 using System.Collections;
 using System.IO;
 using SharpMik;
 using SharpMik.Player;
using Random = UnityEngine.Random;

public class SharpMikManager : Singleton<SharpMikManager> {
 
     internal class UnityVSD : SharpMik.Drivers.VirtualSoftwareDriver {
 
         /**
          * Note: The API for IModDriver has its origins in C APIs where
          * 0 indicates success and nonzero is a specific failure code.
          * 
          * Therefore, functions returning bool return
          *  - true on FAILURE;
          *  - false on SUCCESS.
          */
 
         public AudioSource AudioSource {
             get { return audioSource;}
             set {
                 audioSource = value;
                 applyAudioSource();
             }
         }
 
         AudioSource audioSource { get; set; }
         sbyte[] playerBuffer = new sbyte[0];
         bool playing;
 
         public UnityVSD() {
             m_Name = "Informi SharpMik VSD";
             m_Version = "0.0.1";
             m_HardVoiceLimit = 0;
             m_SoftVoiceLimit = 255;
             m_AutoUpdating = true;
         }
 
         public override void CommandLine(string data) {
             ModDriver.Mode |= SharpMikCommon.DMODE_16BITS;
             ModDriver.Mode |= SharpMikCommon.DMODE_STEREO;
             ModDriver.Mode |= SharpMikCommon.DMODE_INTERP;
 //            ModDriver.Mode |= SharpMikCommon.DMODE_HQMIXER;
 
         }
 
         public override bool IsPresent() {
             int sampleRate = AudioSettings.outputSampleRate;
             if (sampleRate > ushort.MaxValue) {
                 Debug.LogError("Unable to use selected audio source: audio sample rate must be < " + ushort.MaxValue + " (is: " + sampleRate + ")");
                 return false;
             }
             return true;
         }
 
         public override bool Init() {
             if (base.Init()) {
                 Debug.LogError("Underlying virtual driver failed initialization");
                 return true;
             }
 
             ModDriver.MixFrequency = (ushort)AudioSettings.outputSampleRate;
             return false;
         }
 
         public override bool PlayStart() {
             playing = !base.PlayStart();
             syncState();
             return !playing;
         }
 
         public override void PlayStop() {
             base.PlayStop();
             playing = false;
             syncState();
         }
 
         public virtual void Mix32f(float[] dataIO, int channelsRequired, float gain) {
             // We force 16-bit mixing, so we're working in 2-byte chunks.
             uint outLen = (uint)dataIO.Length * 2;
             float normalize = gain / 32768f;
             if (playerBuffer.Length < outLen)
                 playerBuffer = new sbyte[outLen];
 
             uint inLen = WriteBytes(playerBuffer, outLen);
             for (uint w = 0, r = 0; r < inLen; ++w, r += 2) {
                 dataIO[w] = ((playerBuffer[r] & 0xff) | (playerBuffer[r + 1] << 8)) * normalize;
             }
         }
 
         void applyAudioSource() {
             syncState();
         }
 
         void syncState() {
             if (playing) {
                 if (audioSource != null) {
                     audioSource.Play();
                 }
             } else {
                 if (audioSource != null) {
                     audioSource.Stop();
                 }
             }
 
         }
     }
 
     [Range(0f, 2f)]
     public float
         FinalGain = 0.75f;
 
     public TextAsset ModuleAsset;
     public bool IsPlaying { get { return player != null && player.IsPlaying(); } }
 
     public MikMod player;
     UnityVSD driver;
     public Stream songStream;
     TextAsset lastModuleAsset;
 
     void Start() {
         player = new MikMod();
         bool failedInit;
         driver = player.Init<UnityVSD>("command line", out failedInit);
         if (failedInit) {
             player.Exit();
             player = null;
             driver = null;
             return;
         }
 
         driver.AudioSource = Instance.gameObject.GetOrAddComponent<AudioSource>();
         DontDestroyOnLoad(driver.AudioSource);
     }

     private void PlayerOnPlayerStateChangeEvent(ModPlayer.PlayerState state)
     {
	     Debug.Log(state.ToString());
     }

     void OnAudioFilterRead(float[] dataIO, int channelsRequired) {
         if (player == null || driver == null)
             return;

         driver.Mix32f(dataIO, channelsRequired, FinalGain);
     }
 
     void Update() {
         if (lastModuleAsset != ModuleAsset) {
             applyModule(ModuleAsset);                    
             lastModuleAsset = ModuleAsset;
         }
     }
 
     void applyModule(TextAsset apply) {
         if (apply == null) {
             unload();
             return;
         }
 
         if (player == null) {
             Debug.LogError("Cannot play: player failed to initialize");
             return;
         }
         songStream = new MemoryStream(apply.bytes);
         player.Play(songStream);
     }
 
     void unload() {
         player.Stop();
         player.UnLoadCurrent();
         songStream.Close();
     }
 
 
     public void MuteChannel(int channel) {
         if (player == null)
             return;
         player.MuteChannel(channel);
     }
 
     public void UnMuteChannel(int channel) {
         if (player == null)
             return;
         player.UnMuteChannel(channel);
     }
 
 }