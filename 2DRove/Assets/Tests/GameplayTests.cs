using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayTests
{
    // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator LoadProperMap()
    // {
    //     // Load a proper scene that is like a scene we would load normally
    //     SceneManager.LoadScene("Test1", LoadSceneMode.Single);
    //     // Yield next frame to scene
    //     yield return null;

    // }

    // [UnityTest]
    // public IEnumerator LoadNotEnoughPotentialTiles()
    // {
    //     // Assert log message (we are checking for errors)
    //     LogAssert.Expect(LogType.Error, "Cannot request more tiles than available");
    //     // Load scene with too few potential tiles
    //     SceneManager.LoadScene("Test2", LoadSceneMode.Single);
    //     // Yield next frame to scene
    //     yield return null;
    // }
    // [UnityTest]
    // public IEnumerator LoadNotEnoughTiles()
    // {
    //     // Assert log message (we are checking for errors)
    //     LogAssert.Expect(LogType.Error, "must create 1 or more tiles");
    //     // Load scene with not enough tiles to load scene
    //     SceneManager.LoadScene("Test3", LoadSceneMode.Single);
    //     // Yield next frame to scene
    //     yield return null;
    // }
    // [UnityTest]
    // public IEnumerator LoadNotEnoughSize()
    // {
    //     // Assert log message (we are checking for errors)
    //     LogAssert.Expect(LogType.Error, "Scale must be greater than 0");
    //     // Load scene with impossible scale
    //     SceneManager.LoadScene("Test4", LoadSceneMode.Single);
    //     // Yield next frame to scene
    //     yield return null;
    // }
    // /*
    // [UnityTest]
    // public IEnumerator LoadingScreenTest()
    // {
    //     //SceneManager.LoadScene("Test1", LoadSceneMode.Single);

    //     GameObject thing = new GameObject();

    //     LoadingScreenManager loadingScreenManager = thing.AddComponent<LoadingScreenManager>();

    //     loadingScreenManager.LoadScene(5);

    //     yield return new WaitForSeconds(10);
    // }
    // */
    // [UnityTest]
    // public IEnumerator ExitTest()
    // {
    //     // Load a scene so loadingScreenManager can actually load something
    //     SceneManager.LoadScene("Test1", LoadSceneMode.Single);

    //     // Initialize the GameObjects that we need
    //     var triggerObject = new GameObject();
    //     var playerObject = new GameObject("Player");
    //     var loadingObject = new GameObject("LoadingScreenManager");
    //     var mapGenDLAObject = new GameObject("Start");
    //     // Initialize the loading screen manager (this is important in Exit)
    //     var loadingScreenManager = loadingObject.AddComponent<LoadingScreenManager>();
    //     // Set the player object to have the tag so that Exit can detect it
    //     playerObject.tag = "Player";
    //     // Transform the position to the starting spot
    //     triggerObject.transform.position = Vector3.zero;
    //     triggerObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    //     // Initialize an exit component
    //     var exit = triggerObject.AddComponent<Exit>();
    //     // Set our collider in the object to triggered
    //     var collider = triggerObject.AddComponent<BoxCollider2D>();
    //     collider.isTrigger = true;
    //     // Add components to the player object so it can trigger the exit
    //     playerObject.AddComponent<Rigidbody2D>();
    //     playerObject.AddComponent<PolygonCollider2D>();
    //     // Move the player's position to the exit object
    //     playerObject.transform.position = triggerObject.transform.position;

    //     yield return new WaitForSeconds(0.5f);
    // }

    [UnityTest]
    public IEnumerator ArcherTest()
    {
        GameObject enemy = new GameObject();
        GameObject attackPoint = new GameObject("attackPoint");
        attackPoint.AddComponent<Transform>();
        attackPoint.GetComponent<Transform>().position = Vector3.zero;
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<Transform>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<Collider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/ArcherController");
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<ArcherStateManager>();
        ArcherStateManager stateManager = enemy.GetComponent<ArcherStateManager>();
        stateManager.attackPoint = attackPoint.GetComponent<Transform>();
        stateManager.attackRange = 10f;

        ArcherBaseState[] states = {stateManager.SpawnState, stateManager.AggroState, stateManager.AttackState, stateManager.HitState, stateManager.IdleState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (ArcherBaseState state in states)
        {
            if (state == stateManager.AggroState)
            {
                stateManager.SwitchState(state);
                yield return null;
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                stateManager.TakeDamageAnimation();
            }
            else
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.TakeDamageAnimation();
            }

            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator BomberTest()
    {
        GameObject enemy = new GameObject();
        GameObject attackPoint = new GameObject("attackPoint");
        attackPoint.AddComponent<Transform>();
        attackPoint.GetComponent<Transform>().position = Vector3.zero;
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<Transform>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<Collider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/BomberDroid");
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<BomberStateManager>();
        BomberStateManager stateManager = enemy.GetComponent<BomberStateManager>();
        stateManager.attackPointX = attackPoint.GetComponent<Transform>();
        stateManager.attackPointY = attackPoint.GetComponent<Transform>();
        stateManager.attackRange = 10f;

        BomberBaseState[] states = {stateManager.SpawnState, stateManager.AggroState, stateManager.AttackState, stateManager.HitState, stateManager.IdleState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.AddComponent<Transform>();
        stateManager.Player = player.GetComponent<Transform>();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (BomberBaseState state in states)
        {
            if (state == stateManager.AggroState || state == stateManager.AttackState)
            {
                stateManager.SwitchState(state);
                stateManager.EventTrigger();
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                yield return new WaitForSeconds(3.5f);
                stateManager.CollisionTesting(new Collision2D());
                stateManager.TakeDamageAnimation();
            }
            else
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.TakeDamageAnimation();
            }

            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator CagedShockerTest()
    {
        GameObject enemy = new GameObject();
        GameObject attackPoint = new GameObject("attackPoint");
        attackPoint.AddComponent<Transform>();
        attackPoint.GetComponent<Transform>().position = Vector3.zero;
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<Transform>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<Collider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/CagedShocker");
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<CagedShockerStateManager>();
        CagedShockerStateManager stateManager = enemy.GetComponent<CagedShockerStateManager>();
        stateManager.attackPointX = attackPoint.GetComponent<Transform>();
        stateManager.attackPointY = attackPoint.GetComponent<Transform>();
        stateManager.attackRange = 10f;

        CagedShockerBaseState[] states = {stateManager.SpawnState, stateManager.Lurch1State, stateManager.Lurch2State, stateManager.AttackState, stateManager.HitState, stateManager.IdleState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.AddComponent<Transform>();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (CagedShockerBaseState state in states)
        {
            stateManager.SwitchState(state);
            stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
            stateManager.EventTrigger();
            stateManager.CollisionTesting(new Collision2D());
            yield return null;
            stateManager.TakeDamageAnimation();
            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator CagedSpiderTest()
    {
        GameObject enemy = new GameObject();
        GameObject attackPoint = new GameObject("attackPoint");
        attackPoint.AddComponent<Transform>();
        attackPoint.GetComponent<Transform>().position = Vector3.zero;
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<Transform>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<Collider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/CagedSpider");
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<SpiderStateManager>();
        SpiderStateManager stateManager = enemy.GetComponent<SpiderStateManager>();
        stateManager.attackPoint = attackPoint.GetComponent<Transform>();
        stateManager.attackRange = 10f;

        SpiderBaseState[] states = {stateManager.SleepState, stateManager.AggroState, stateManager.AttackState, stateManager.HitState, stateManager.IdleState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (SpiderBaseState state in states)
        {
            if (state == stateManager.AggroState)
            {
                stateManager.SwitchState(state);
                yield return null;
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                stateManager.TakeDamageAnimation();
            }
            else
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.TakeDamageAnimation();
            }

            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator DaggerMushroomTest()
    {
        GameObject enemy = new GameObject();
        GameObject attackPoint = new GameObject("attackPoint");
        attackPoint.AddComponent<Transform>();
        attackPoint.GetComponent<Transform>().position = Vector3.zero;
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<Transform>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<Collider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/DaggerMushroom");
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<DaggerMushroomStateManager>();
        DaggerMushroomStateManager stateManager = enemy.GetComponent<DaggerMushroomStateManager>();
        stateManager.attackPoint = attackPoint.GetComponent<Transform>();
        stateManager.attackRange = 10f;

        DaggerMushroomBaseState[] states = {stateManager.SpawnState, stateManager.AggroState, stateManager.AttackState, stateManager.HitState, stateManager.IdleState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (DaggerMushroomBaseState state in states)
        {
            if (state == stateManager.AggroState)
            {
                stateManager.SwitchState(state);
                yield return null;
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                stateManager.TakeDamageAnimation();
            }
            else
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.TakeDamageAnimation();
            }

            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator ElkTest()
    {
        GameObject enemy = new GameObject();
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<Transform>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<Collider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/ElkBlue");
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<ElkStateManager>();
        ElkStateManager stateManager = enemy.GetComponent<ElkStateManager>();

        ElkBaseState[] states = {stateManager.SpawnState, stateManager.IdleState, stateManager.EatState, stateManager.AlertState, stateManager.FleeState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (ElkBaseState state in states)
        {
            if (state == stateManager.AlertState || state == stateManager.FleeState)
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.TakeDamageAnimation();
                yield return null;
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
            }
            else
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.TakeDamageAnimation();
            }

            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator FoxTest()
    {
        GameObject enemy = new GameObject();
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<Transform>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<Collider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Fox");
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<FoxStateManager>();
        FoxStateManager stateManager = enemy.GetComponent<FoxStateManager>();

        FoxBaseState[] states = {stateManager.SpawnState, stateManager.IdleState, stateManager.EatState, stateManager.AlertState, stateManager.FleeState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (FoxBaseState state in states)
        {
            if (state == stateManager.AlertState || state == stateManager.FleeState)
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.TakeDamageAnimation();
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.EventTrigger();
            }
            else
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.TakeDamageAnimation();
            }

            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator GhoulTest()
    {
        GameObject enemy = new GameObject();
        GameObject attackPoint = new GameObject("attackPoint");
        attackPoint.AddComponent<Transform>();
        attackPoint.GetComponent<Transform>().position = Vector3.zero;
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<Transform>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<Collider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Ghoul");
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<GhoulStateManager>();
        GhoulStateManager stateManager = enemy.GetComponent<GhoulStateManager>();
        stateManager.attackPoint = attackPoint.GetComponent<Transform>();
        stateManager.attackRange = 10f;

        GhoulBaseState[] states = {stateManager.SpawnState, stateManager.AggroState, stateManager.AttackState, stateManager.HitState, stateManager.IdleState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (GhoulBaseState state in states)
        {
            if (state == stateManager.AggroState)
            {
                stateManager.SwitchState(state);
                yield return null;
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                stateManager.TakeDamageAnimation();
            }
            else
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.TakeDamageAnimation();
            }

            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator SpitterTest()
    {
        GameObject enemy = new GameObject();
        GameObject attackPoint = new GameObject("attackPoint");
        attackPoint.AddComponent<Transform>();
        attackPoint.GetComponent<Transform>().position = Vector3.zero;
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<Transform>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<Collider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Spitter");
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<SpitterStateManager>();
        SpitterStateManager stateManager = enemy.GetComponent<SpitterStateManager>();
        stateManager.attackPoint = attackPoint.GetComponent<Transform>();
        stateManager.attackRange = 10f;
        stateManager.projectilePrefab = Resources.Load<GameObject>("Prefabs/SpitterRangeAttack");
        stateManager.projectileSpawnPoint = attackPoint.transform;

        SpitterBaseState[] states = {stateManager.SpawnState, stateManager.AggroState, stateManager.AttackState, stateManager.HitState, stateManager.IdleState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (SpitterBaseState state in states)
        {
            if (state == stateManager.AggroState)
            {
                stateManager.SwitchState(state);
                yield return null;
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                stateManager.TakeDamageAnimation();
            }
            else
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.TakeDamageAnimation();
            }

            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator SummonerTest()
    {
        GameObject enemy = new GameObject();
        GameObject attackPoint = new GameObject("attackPoint");
        attackPoint.AddComponent<Transform>();
        attackPoint.GetComponent<Transform>().position = Vector3.zero;
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<Transform>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<Collider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Summoner");
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<SummonerStateManager>();
        SummonerStateManager stateManager = enemy.GetComponent<SummonerStateManager>();

        SummonerBaseState[] states = {stateManager.SpawnState, stateManager.AggroState, stateManager.SummoningState, stateManager.HitState, stateManager.IdleState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (SummonerBaseState state in states)
        {
            if (state == stateManager.AggroState)
            {
                stateManager.SwitchState(state);
                yield return null;
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                stateManager.TakeDamageAnimation();
            }
            else
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.TakeDamageAnimation();
            }

            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator WardenTest()
    {
        GameObject enemy = new GameObject();
        GameObject attackPoint = new GameObject("attackPoint");
        attackPoint.AddComponent<Transform>();
        attackPoint.GetComponent<Transform>().position = Vector3.zero;
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<Transform>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<Collider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Warden");
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<WardenStateManager>();
        WardenStateManager stateManager = enemy.GetComponent<WardenStateManager>();
        stateManager.attackPointX = attackPoint.GetComponent<Transform>();
        stateManager.attackPointY = attackPoint.GetComponent<Transform>();

        WardenBaseState[] states = {stateManager.SpawnState, stateManager.AggroState, stateManager.AttackState, stateManager.HitState, stateManager.IdleState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (WardenBaseState state in states)
        {
            if (state == stateManager.AggroState)
            {
                stateManager.SwitchState(state);
                yield return null;
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                stateManager.TakeDamageAnimation();
            }
            else
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.TakeDamageAnimation();
            }

            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator GuardianTest()
    {
        GameObject enemy = new GameObject();
        GameObject attackPoint = new GameObject("attackPoint");
        attackPoint.AddComponent<Transform>();
        attackPoint.GetComponent<Transform>().position = Vector3.zero;
        enemy.AddComponent<Transform>();
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<BoxCollider2D>();
        enemy.AddComponent<CapsuleCollider2D>();
        enemy.AddComponent<CircleCollider2D>();
        enemy.AddComponent<PolygonCollider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Guardian");
        enemy.AddComponent<AfterImage>();
        enemy.GetComponent<AfterImage>().ghostDelay = 1f;
        enemy.GetComponent<AfterImage>().ghostDuration = 1f;
        enemy.GetComponent<AfterImage>().ghost = Resources.Load<GameObject>("Prefabs/GuardianAfterImage");
        enemy.GetComponent<AfterImage>().makeGhost = false;
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<GuardianStateManager>();
        GuardianStateManager stateManager = enemy.GetComponent<GuardianStateManager>();
        stateManager.attack1 = attackPoint.GetComponent<Transform>();
        stateManager.attack1Length = 10f;
        stateManager.attack2 = attackPoint.GetComponent<Transform>();
        stateManager.attack2Range = 10f;
        stateManager.MovementSpeed = 1;
        stateManager.walkAnimSpeed = 1;
        stateManager.attack1Speed = 1;
        stateManager.attack2Speed = 1;
        stateManager.vertDashSpeed = 1;
        stateManager.horizontalDashSpeed = 1;
        stateManager.AoESpeed = 1;
        stateManager.AoEResetSpeed = 1;
        stateManager.attack1Time = 1;
        stateManager.attack2Time = 1;
        stateManager.vertDashTime = 1;
        stateManager.horizontalDashTime = 1;
        stateManager.AoETime = 1;
        stateManager.AoEResetTime = 1;

        GuardianBaseState[] states = {stateManager.SpawnState, stateManager.AggroState, stateManager.AttackState, stateManager.VertDashState, stateManager.HorizontalDashState, stateManager.SpecialState, stateManager.IdleState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (GuardianBaseState state in states)
        {
            if (state == stateManager.AggroState)
            {
                stateManager.SwitchState(state);
                yield return null;
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger(1);
                stateManager.EventTrigger(2);
                stateManager.EventTrigger(3);
                stateManager.CollisionTesting(new Collision2D());
                stateManager.TakeDamageAnimation();
            }
            else
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger(1);
                stateManager.EventTrigger(2);
                stateManager.EventTrigger(3);
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.TakeDamageAnimation();
            }

            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator RockRobotBossTest()
    {
        GameObject enemy = new GameObject();
        GameObject attackPoint = new GameObject("attackPoint");
        attackPoint.AddComponent<Transform>();
        attackPoint.GetComponent<Transform>().position = Vector3.zero;
        enemy.AddComponent<Transform>();
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<BoxCollider2D>();
        enemy.AddComponent<CapsuleCollider2D>();
        enemy.AddComponent<CircleCollider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/RockBoss");
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<RockBossStateManager>();
        RockBossStateManager stateManager = enemy.GetComponent<RockBossStateManager>();
        stateManager.MovementSpeed = 1f;
        stateManager.walkAnimSpeed = 1f;
        stateManager.rangeAttackSpeed = 1f;
        stateManager.attackSpeed = 1f;
        stateManager.burstSpeed = 1f;
        stateManager.buffSpeed = 1f;
        stateManager.attackPointX = attackPoint.GetComponent<Transform>();
        stateManager.attackPointY = attackPoint.GetComponent<Transform>();
        stateManager.rangeAttackTime = 1f;
        stateManager.attackTime = 1f;
        stateManager.burstTime = 1f;
        stateManager.buffTime = 1f;
        stateManager.attackRange = 1f;
        stateManager.attackHeight = 1f;

        RockBossBaseState[] states = {stateManager.SpawnState, stateManager.AggroState, stateManager.AttackState, stateManager.HitState, stateManager.IdleState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (RockBossBaseState state in states)
        {
            if (state == stateManager.AggroState)
            {
                stateManager.SwitchState(state);
                yield return null;
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                stateManager.TakeDamageAnimation();
            }
            else
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.TakeDamageAnimation();
            }

            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator TarnishedWidowTest()
    {
        GameObject enemy = new GameObject();
        GameObject attackPoint = new GameObject("attackPoint");
        attackPoint.AddComponent<Transform>();
        attackPoint.GetComponent<Transform>().position = Vector3.zero;
        enemy.AddComponent<Transform>();
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<NewEnemy>();
        enemy.AddComponent<BoxCollider2D>();
        enemy.AddComponent<CapsuleCollider2D>();
        enemy.AddComponent<CircleCollider2D>();
        enemy.GetComponent<NewEnemy>().maxHealth = 10;
        enemy.AddComponent<Animator>();
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/widowAnimationController");
        if (enemy.GetComponent<Animator>().runtimeAnimatorController == null)
        {
            // RuntimeAnimatorController exists
            Debug.Log("RuntimeAnimatorController DNE.");
        }

        enemy.AddComponent<WidowStateManager>();
        WidowStateManager stateManager = enemy.GetComponent<WidowStateManager>();
        stateManager.MovementSpeed = 1f;
        stateManager.walkAnimSpeed = 1f;
        stateManager.attackSpeed = 1f;
        stateManager.spitSpeed = 1f;
        stateManager.jumpSpeed = 1f;
        stateManager.attackPointX = attackPoint.GetComponent<Transform>();
        stateManager.attackPointY = attackPoint.GetComponent<Transform>();
        stateManager.attackTime = 1f;
        stateManager.spitTime = 1f;
        stateManager.jumpTime = 1f;
        stateManager.attackRange = 1f;
        stateManager.attackHeight = 1f;

        WidowBaseState[] states = {stateManager.SpawnState, stateManager.AggroState, stateManager.AttackState, stateManager.HitState, stateManager.IdleState, stateManager.DeathState};
        GameObject UIOverlay = new GameObject("UI Overlay");
        UIOverlay.AddComponent<GameOverMenu>();

        // GameObject HealthText = new GameObject("Health Text");
        // HealthText.AddComponent<TMP_Text>();

        GameObject GoldText = new GameObject("Gold Text");
        GoldText.AddComponent<TMP_Text>();

        // GameObject HealthSlider = new GameObject("Health Slider");
        // HealthText.AddComponent<Slider>();

        GameObject player = new GameObject();
        player.gameObject.tag = "Player";
        player.gameObject.layer = 7; //layer == player == 7
        player.AddComponent<PlayerController>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Animator>();
        player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CompletedControllers/Player");
        player.AddComponent<Rigidbody2D>();

        foreach (WidowBaseState state in states)
        {
            if (state == stateManager.AggroState)
            {
                stateManager.SwitchState(state);
                yield return null;
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                stateManager.TakeDamageAnimation();
            }
            else
            {
                stateManager.SwitchState(state);
                stateManager.TriggerTesting(player.GetComponent<BoxCollider2D>());
                stateManager.EventTrigger();
                stateManager.CollisionTesting(new Collision2D());
                yield return null;
                stateManager.TakeDamageAnimation();
            }

            if (state == stateManager.DeathState)
            {
                stateManager.Destroy(.5f);
            }
        }

        yield return null;
    }
}