using System;
using System.Collections.Generic;
using System.Threading;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public bool Running = true;
    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }


    public void Setup()
    {
      //create all my rooms
      Room hypersleep = new Room("The Hypersleep Chamber. Back of the ship.", "You see the door to the cafeteria to the west, the viewport on the door is smeared with blood.");
      Room cafeteria = new Room("The Cafeteria. Back of the ship", "The door slides sideways to allow your entry. There are bloodstains everywhere, but not a single body to be found. The lights continue to flicker. There is evidence of a struggle with turned over tables, scorchmarks, and small unexplained holes burned through the floor. There are two streaks of blood headed west to the door to the living quarters. The door seems to be stuck in some kind of loop opening partially and closing. inidicating something or someone was either dragged or crawled to that door. The door to the north leads to the main hallway leading closer to the bridge. The view through the viewport is dark.");
      Room livingQuarters = new Room("The Living Quarters. Middle of the ship", "fill in later");
      Room hall = new Room("", "");
      Room engine = new Room("", "");
      Room science = new Room("", "");
      Room armory = new Room("", "");
      Room bridge = new Room("", "");


      //set up their relationships
      hypersleep.Exits.Add("west", cafeteria);
      cafeteria.Exits.Add("west", livingQuarters);
      cafeteria.Exits.Add("north", hall);
      engine.Exits.Add("west", hall);
      engine.Exits.Add("north", science);
      livingQuarters.Exits.Add("east", hall);
      livingQuarters.Exits.Add("north", armory);
      armory.Exits.Add("east", science);
      armory.Exits.Add("north", bridge);

      Item flamethrower = new Item("Flamethrower", "Keep out of reach of children");
      armory.Items.Add(flamethrower);






      CurrentRoom = hypersleep;
    }
    public void GetUserInput()
    {
      string[] choice = Console.ReadLine().ToLower().Split(" ");
      string command = choice[0];
      string dir = "";
      if (choice.Length > 1)
      {
        dir = choice[1];

      }
      switch (command)
      {
        case "go":
          Go(dir);
          break;

        case "help":
          Help();
          break;

        case "inventory":
          Inventory();
          break;

        case "look":
          Look();
          break;

        case "quit":
          Quit();
          break;

        case "reset":
          Reset();
          break;

        case "take":
          TakeItem(dir);
          break;

        case "use":
          UseItem(dir);
          break;

        default:
          System.Console.WriteLine("Not Possible");
          break;
      }
    }

    public void Go(string input)
    {

    }

    public void Help()
    {
      System.Console.WriteLine(@"
 ▄         ▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄            ▄▄▄▄▄▄▄▄▄▄▄ 
▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░▌          ▐░░░░░░░░░░░▌
▐░▌       ▐░▌▐░█▀▀▀▀▀▀▀▀▀ ▐░▌          ▐░█▀▀▀▀▀▀▀█░▌
▐░▌       ▐░▌▐░▌          ▐░▌          ▐░▌       ▐░▌
▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄▄▄ ▐░▌          ▐░█▄▄▄▄▄▄▄█░▌
▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░▌          ▐░░░░░░░░░░░▌
▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀▀▀ ▐░▌          ▐░█▀▀▀▀▀▀▀▀▀ 
▐░▌       ▐░▌▐░▌          ▐░▌          ▐░▌          
▐░▌       ▐░▌▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄▄▄ ▐░▌          
▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░▌          
 ▀         ▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀          
      While playing the game you may use the following commands:
      Go - type go and one of the compass directions to move between rooms
      Help - brings you here, same in every room
      Inventory - check the items currently available in your inventory
      Look - look around the room
      Quit - exit the game in fear
      Reset - reset the game
      Take - in a room with an available item, add that item to your inventory
      use - use item in your inventory

      ");
    }

    public void Inventory()
    {
      throw new System.NotImplementedException();
    }

    public void Look()
    {
      Console.Clear();
      System.Console.Write($"{CurrentRoom.Description}");
    }

    public void Quit()
    {
      System.Console.WriteLine(@"
 ▄▄▄▄▄▄▄▄▄▄▄  ▄         ▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄ 
▐░░░░░░░░░░░▌▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌
▐░█▀▀▀▀▀▀▀█░▌▐░▌       ▐░▌ ▀▀▀▀█░█▀▀▀▀  ▀▀▀▀█░█▀▀▀▀ 
▐░▌       ▐░▌▐░▌       ▐░▌     ▐░▌          ▐░▌     
▐░▌       ▐░▌▐░▌       ▐░▌     ▐░▌          ▐░▌     
▐░▌       ▐░▌▐░▌       ▐░▌     ▐░▌          ▐░▌     
▐░█▄▄▄▄▄▄▄█░▌▐░▌       ▐░▌     ▐░▌          ▐░▌     
▐░░░░░░░░░░░▌▐░▌       ▐░▌     ▐░▌          ▐░▌     
 ▀▀▀▀▀▀█░█▀▀ ▐░█▄▄▄▄▄▄▄█░▌ ▄▄▄▄█░█▄▄▄▄      ▐░▌     
        ▐░▌  ▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌     ▐░▌     
         ▀    ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀       ▀      
      Are you sure you want to quit the current game? You will be launched into the void of space. (Y/N)?");
      if (Console.ReadLine() == "y".ToLower())
      {
        Running = false;
      }
      else
      {
        Look();
      }
    }

    public void Reset()
    {
      System.Console.WriteLine(@"
 ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄ 
▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌
▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀  ▀▀▀▀█░█▀▀▀▀ 
▐░▌       ▐░▌▐░▌          ▐░▌          ▐░▌               ▐░▌     
▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄▄▄      ▐░▌     
▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌     ▐░▌     
▐░█▀▀▀▀█░█▀▀ ▐░█▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀▀▀      ▐░▌     
▐░▌     ▐░▌  ▐░▌                    ▐░▌▐░▌               ▐░▌     
▐░▌      ▐░▌ ▐░█▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄▄▄      ▐░▌     
▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌     ▐░▌     
 ▀         ▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀       ▀      
Are you sure you wish to start over from the beginning?");
      if (Console.ReadLine() == "y".ToLower())
      {
        Setup();
        StartGame();

      }
      else
      {
        Look();
      }
    }


    public void StartGame()
    {
      while (Running)
      {
        Console.Clear();
        System.Console.WriteLine($"{CurrentRoom.Description}");
        GetUserInput();

      }



    }


    public void TakeItem(string itemName)
    {
      throw new System.NotImplementedException();
    }

    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }
    public GameService()
    {
      Setup();

    }
  }
}