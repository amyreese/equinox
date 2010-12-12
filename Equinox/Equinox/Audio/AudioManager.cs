using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Equinox.Audio
{
    class AudioManager : GameComponent
    {
        private AudioEngine engine;

        private SoundBank sfxSoundBank;
        private WaveBank sfxWaveBank;

        private SoundBank musicSoundBank;
        private WaveBank musicWaveBank;

        /// <summary>
        /// Initialize the audio manager.
        /// </summary>
        public AudioManager() : base(Engine.game)
        {
            engine = new AudioEngine("Resources/Audio/Equinox.xgs");

            sfxWaveBank = new WaveBank(engine, "Resources/Audio/Sound.xwb");
            sfxSoundBank = new SoundBank(engine, "Resources/Audio/Sound.xsb");

            musicWaveBank = new WaveBank(engine, "Resources/Audio/Music.xwb", 0, (short) 16);
            musicSoundBank = new SoundBank(engine, "Resources/Audio/Music.xsb");

            engine.Update();
        }

        private Cue currentMusicTrack;

        /// <summary>
        /// Set the current music track, stopping any existing music track if necessary.
        /// </summary>
        /// <param name="trackName">New track name</param>
        public void MusicTrack(string trackName)
        {
            if (currentMusicTrack != null)
            {
                if (!(currentMusicTrack.IsStopping || currentMusicTrack.IsStopped))
                {
                    currentMusicTrack.Stop(AudioStopOptions.AsAuthored);
                }
            }

            currentMusicTrack = musicSoundBank.GetCue(trackName);
            currentMusicTrack.Play();
        }

        /// <summary>
        /// Play a one-off sound effect.
        /// </summary>
        /// <param name="soundName">Sound effect name</param>
        public void PlaySound(string soundName)
        {
            Cue sound = sfxSoundBank.GetCue(soundName);
            sound.Play();
        }

        /// <summary>
        /// Update the audio engine state.
        /// </summary>
        public void Update()
        {
            engine.Update();
        }
    }
}
