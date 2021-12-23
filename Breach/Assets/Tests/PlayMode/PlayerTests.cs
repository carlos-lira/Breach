using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTests
    {

        private GameObject coreGameObject;

        [UnityTest]
        public IEnumerator DamagePlayer_NonLethal()
        {
            if (coreGameObject == null)
                InitGameDependencies();

            var gameObject = new GameObject();
            gameObject.AddComponent<AudioSource>();
            var player = gameObject.AddComponent<Player>();

            var startingHealth = player.CurrentHealth();
            player.DamagePlayer(10f);

            Assert.AreEqual(startingHealth - 10f, player.CurrentHealth());
            Assert.AreEqual(true, player.Alive());

            GameObject.Destroy(gameObject);

            yield return null;
        }

        [UnityTest]
        public IEnumerator DamagePlayer_Lethal()
        {
            if (coreGameObject == null)
                InitGameDependencies();

            var gameObject = new GameObject();
            gameObject.AddComponent<AudioSource>();
            var player = gameObject.AddComponent<Player>();

            var initialHealth = player.CurrentHealth();

            player.DamagePlayer(initialHealth + 500f);

            Assert.LessOrEqual(player.CurrentHealth(), 0f);
            Assert.AreEqual(false, player.Alive());

            GameObject.Destroy(gameObject);

            yield return null;
        }

        [UnityTest]
        public IEnumerator WithdrawMoney()
        {
            if (coreGameObject == null)
                InitGameDependencies();

            var gameObject = new GameObject();
            gameObject.AddComponent<AudioSource>();
            var player = gameObject.AddComponent<Player>();

            var startingMoney = player.MoneyAvailable();

            player.WithdrawMoney(200f);

            Assert.AreEqual(startingMoney - 200f, player.MoneyAvailable());

            GameObject.Destroy(gameObject);

            yield return null;
        }

        private void InitGameDependencies()
        {
            if (coreGameObject == null)
                coreGameObject = new GameObject();


            coreGameObject = new GameObject();
            coreGameObject.AddComponent<AudioSource>();
            coreGameObject.AddComponent<Camera>();

            GameObject uiGameObject = new GameObject();
            var sm = coreGameObject.AddComponent<SoundManager>();
            sm.musicAudioSource = coreGameObject.GetComponent<AudioSource>();
            sm.sfxAudioSource = coreGameObject.GetComponent<AudioSource>();
            var lm = coreGameObject.AddComponent<LevelManager>();
            lm.mainCamera = coreGameObject.GetComponent<Camera>();
            lm.finishCamera = coreGameObject.GetComponent<Camera>();
            lm.pauseUI = uiGameObject;
            lm.levelClearedUI = uiGameObject;
            lm.inGameUI = uiGameObject;
            lm.gameOverUI = uiGameObject;
        }


    }
}
