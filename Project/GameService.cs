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
      Room livingQuarters = new Room("The Living Quarters. Middle of the ship", "Living Quarters");
      Room hall = new Room("The Main Hallway. Middle of the Ship", "Hallway");
      Room engine = new Room("The Engineering Room, Middle of the Ship", "Engine Room");
      Room science = new Room("The Science Room, Top of the Ship", "Science");
      Room armory = new Room("The Armory Room, Top of the Ship", "Armory");
      Room bridge = new Room("The Bridge", "End");


      //set up their relationships
      hypersleep.Exits.Add("west", cafeteria);
      cafeteria.Exits.Add("west", livingQuarters);
      cafeteria.Exits.Add("north", hall);
      cafeteria.Exits.Add("east", hypersleep);
      engine.Exits.Add("west", hall);
      engine.Exits.Add("north", science);
      livingQuarters.Exits.Add("east", hall);
      livingQuarters.Exits.Add("north", armory);
      livingQuarters.Exits.Add("south", cafeteria);
      armory.Exits.Add("east", science);
      armory.Exits.Add("north", bridge);
      armory.Exits.Add("south", livingQuarters);
      hall.Exits.Add("west", livingQuarters);
      hall.Exits.Add("east", engine);
      hall.Exits.Add("south", cafeteria);


      Item flamethrower = new Item("Flamethrower", "Keep out of reach of children");
      armory.Items.Add(flamethrower);






      CurrentRoom = hypersleep;
    }
    public void GetUserInput()
    {
      string[] choice = Console.ReadLine().ToLower().Split(" ");
      string command = choice[0];
      string dir = choice[1];
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
          Thread.Sleep(2000);
          break;
      }
    }

    public void Go(string dir)
    {
      if (CurrentRoom.Exits.ContainsKey(dir))
      {
        CurrentRoom = (Room)CurrentRoom.Exits[dir];
        Look();
      }
      else
      {
        System.Console.WriteLine("Nothing there");
        Thread.Sleep(2000);

      }
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
      Press enter to go back to game"
      );
      Console.ReadLine();
      Look();
    }

    public void Inventory()
    {
      if (CurrentPlayer.Inventory.Count > 0)
      {
        CurrentPlayer.Inventory.ForEach(f =>
        {
          System.Console.WriteLine(f.Name);
          System.Console.WriteLine(f.Description);
        });
      }
      else
      {
        System.Console.WriteLine("you have no items");
      }

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
        Look();
        System.Console.WriteLine();
        GetUserInput();

      }



    }


    public void TakeItem(string itemName)
    {
      Item flamethrower = CurrentRoom.Items.Find(i =>
 {
   return i.Name.ToLower() == itemName;
 });
      if (flamethrower != null)
      {
        CurrentRoom.Items.Remove(flamethrower);
        CurrentPlayer.Inventory.Add(flamethrower);
        System.Console.WriteLine("You take the flamethrower");
      }
    }

    public void UseItem(string itemName)
    {

    }
    public GameService(Player name)
    {
      Setup();
      CurrentPlayer = name;

    }
  }
}