using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
//using Player;

public class PlayerScriptTest
{
    private Player player;

    [SetUp]
    public void Setup()
    {
        // Initialize player with default values for each test
        player = new Player("TestPlayer", null);
    }

    [Test]
    public void PlayerStartsInDefensePhase()
    {
        // Verify that the default phase is Defense
        Assert.AreEqual(Player.Phase.Defense, player.GetCurrentPhase());
    }

    [Test]
    public void NextPhaseCyclesThroughPhasesCorrectly()
    {
        // Defense -> Attack
        player.NextPhase();
        Assert.AreEqual(Player.Phase.Attack, player.GetCurrentPhase());

        // Attack -> Build
        player.NextPhase();
        Assert.AreEqual(Player.Phase.Build, player.GetCurrentPhase());

        // Build -> Defense (Loop back to start)
        player.NextPhase();
        Assert.AreEqual(Player.Phase.Defense, player.GetCurrentPhase());
    }

    [Test]
    public void ResetPhaseSetsPhaseToDefense()
    {
        // Move to a different phase
        player.NextPhase(); // Move to Attack
        player.NextPhase(); // Move to Build

        // Reset back to Defense
        player.ResetPhase();
        Assert.AreEqual(Player.Phase.Defense, player.GetCurrentPhase());
    }

    [Test]
    public void PhaseTransitionIsConsistentAcrossMultipleCycles()
    {
        // Go through several cycles to check for consistency
        for (int i = 0; i < 10; i++)
        {
            Assert.AreEqual(Player.Phase.Defense, player.GetCurrentPhase());
            player.NextPhase();
            Assert.AreEqual(Player.Phase.Attack, player.GetCurrentPhase());
            player.NextPhase();
            Assert.AreEqual(Player.Phase.Build, player.GetCurrentPhase());
            player.NextPhase();
        }
    }

    // A Test behaves as an ordinary method
    [Test]
    public void testPlayerConstructor()
    {
        Material color = null;
        Player p1 = new Player("ogre", color);
        p1.PlayerFinishTurn();

        // Make sure class is created with correct parameters/variables
        Assert.AreEqual(p1.GetName(), "ogre");
        Assert.AreEqual(p1.CalculatePercentage(), 0);
        Assert.AreEqual(p1.IsPlayerTurnFinished(), true);
    }

    [Test]
    public void testPlayerVariables()
    {
        Material color = null;
        Player p1 = new Player("ogre", color);
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer rend = cube.GetComponent<Renderer>();
        Material playerColor = new Material(Shader.Find("Specular"));

        p1.SetColor(playerColor);
        // Make sure class is created with correct parameters/variables
        Assert.AreEqual(p1.GetColor(), playerColor);

        p1.SetName("Bob");
        Assert.AreEqual(p1.GetName(), "Bob");

        Tile.TileReference tile = new Tile.TileReference();
        p1.AddTiles(ref tile);
        Assert.AreEqual(p1.GetTiles().Count, 1);
        Assert.AreEqual(p1.getTilesControlledCount(), 1);
    }
}