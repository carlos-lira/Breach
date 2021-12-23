using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class EnemiesTests
    {

        private GameObject coreGameObject;


        [UnityTest]
        public IEnumerator EnemiesDamage_NonLethal()
        {
            if (coreGameObject == null)
                InitGameDependencies();

            var gameObject = new GameObject();
            gameObject.AddComponent<Renderer>();
            gameObject.AddComponent<Animator>();


            var enemy = gameObject.AddComponent<Enemy>();
            enemy.resistanceType = AttackType.NONE;
            enemy.health = 200f;

            enemy.Damage(100f,AttackType.PHYSICAL);

            Assert.AreEqual(100f, enemy.health);
            Assert.AreEqual(true, enemy.IsAlive());

            GameObject.Destroy(gameObject);
            
            yield return null;
            
        }

        [UnityTest]
        public IEnumerator EnemiesDamage_Lethal()
        {
            if (coreGameObject == null)
                InitGameDependencies();

            var gameObject = new GameObject();
            gameObject.AddComponent<Renderer>();
            gameObject.AddComponent<Animator>();


            var enemy = gameObject.AddComponent<Enemy>();
            enemy.resistanceType = AttackType.NONE;
            enemy.health = 200f;

            enemy.Damage(300f, AttackType.PHYSICAL);

            Assert.LessOrEqual(enemy.health, 0f);
            Assert.AreEqual(false, enemy.IsAlive());

            GameObject.Destroy(gameObject);

            yield return null;
        }

        [UnityTest]
        public IEnumerator EnemiesDamage_Resistance()
        {
            if (coreGameObject == null)
                InitGameDependencies();

            var gameObject = new GameObject();
            gameObject.AddComponent<Renderer>();
            gameObject.AddComponent<Animator>();


            var enemy = gameObject.AddComponent<Enemy>();
            enemy.resistanceType = AttackType.PHYSICAL;
            enemy.resistanceFactor = .5f;
            enemy.health = 200f;

            enemy.Damage(200f, AttackType.PHYSICAL);

            Assert.AreEqual(100f, enemy.health, 100f);
            Assert.AreEqual(true, enemy.IsAlive());

            GameObject.Destroy(gameObject);

            yield return null;
        }

        [UnityTest]
        public IEnumerator EnemiesDamage_Burn()
        {
            if (coreGameObject == null)
                InitGameDependencies();

            var gameObject = new GameObject();
            gameObject.AddComponent<Renderer>();
            gameObject.AddComponent<Animator>();


            var enemy = gameObject.AddComponent<Enemy>();
            enemy.resistanceType = AttackType.NONE;
            enemy.health = 200f;

            enemy.Damage(0f, AttackType.FIRE, 20f, 5, 1f);

            Assert.AreEqual(true, enemy.IsBurned());
            Assert.AreEqual(180f, enemy.health);
            yield return new WaitForSeconds(1f / 5);
            Assert.AreEqual(true, enemy.IsBurned());
            Assert.AreEqual(160f, enemy.health);
            yield return new WaitForSeconds(1f / 5);
            Assert.AreEqual(true, enemy.IsBurned());
            Assert.AreEqual(140f, enemy.health);
            yield return new WaitForSeconds(1f / 5);
            Assert.AreEqual(true, enemy.IsBurned());
            Assert.AreEqual(120f, enemy.health);
            yield return new WaitForSeconds(1f / 5);
            Assert.AreEqual(true, enemy.IsBurned());
            Assert.AreEqual(100f, enemy.health);
            yield return new WaitForSeconds(1f / 5);
            Assert.AreEqual(false, enemy.IsBurned());
            Assert.AreEqual(100f, enemy.health);

            Assert.AreEqual(enemy.IsAlive(), true);

            GameObject.Destroy(gameObject);

            yield return null;
        }

        [UnityTest]
        public IEnumerator EnemiesSlow_NoResistance()
        {
            if (coreGameObject == null)
                InitGameDependencies();

            var gameObject = new GameObject();
            gameObject.AddComponent<Renderer>();
            gameObject.AddComponent<Animator>();


            var enemy = gameObject.AddComponent<Enemy>();
            enemy.initialSpeed = 10f;
            enemy.BackToNormalSpeed();
            enemy.resistanceType = AttackType.PHYSICAL;
            enemy.resistanceFactor = .5f;
            enemy.health = 200f;

            enemy.ReduceSpeed(1f, AttackType.ARCANE);
            Assert.AreEqual(0f, enemy.GetCurrentSpeed());
            enemy.BackToNormalSpeed();
            Assert.AreEqual(10f, enemy.GetCurrentSpeed());

            GameObject.Destroy(gameObject);

            yield return null;
        }

        [UnityTest]
        public IEnumerator EnemiesSlow_Resistance()
        {
            if (coreGameObject == null)
                InitGameDependencies();

            var gameObject = new GameObject();
            gameObject.AddComponent<Renderer>();
            gameObject.AddComponent<Animator>();


            var enemy = gameObject.AddComponent<Enemy>();
            enemy.initialSpeed = 10f;
            enemy.BackToNormalSpeed();
            enemy.resistanceType = AttackType.ARCANE;
            enemy.resistanceFactor = .5f;
            enemy.health = 200f;

            enemy.ReduceSpeed(1f, AttackType.ARCANE);
            Assert.AreEqual(5f, enemy.GetCurrentSpeed());
            enemy.BackToNormalSpeed();
            Assert.AreEqual(10f, enemy.GetCurrentSpeed());

            GameObject.Destroy(gameObject);

            yield return null;
        }

        private void InitGameDependencies()
        {
            coreGameObject = new GameObject();
            
            //DestroyGameDependencies();

            coreGameObject.AddComponent<AudioSource>();
            coreGameObject.AddComponent<Camera>();
            coreGameObject.AddComponent<Player>();

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


            GameObject waypoint1 = new GameObject();
            waypoint1.transform.parent = coreGameObject.transform;
            waypoint1.transform.position = Vector3.zero;
            GameObject waypoint2 = new GameObject();
            waypoint2.transform.parent = coreGameObject.transform;
            waypoint2.transform.position = new Vector3(0, 0, 5);
           
            
            coreGameObject.AddComponent<Waypoints>();
        }

        private void DestroyGameDependencies()
        {
            foreach (var comp in coreGameObject.GetComponents<Component>())
            {
                GameObject.Destroy(comp);
            }
        }


    }
}
