using System;
using CastleGrimtol.Project;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.Clear();
      System.Console.Write(@"
                           |-----------|
           i               |===========|                       
           |               |,---------.|                      __--~\__--.
    #---,'""""`-_   `n     |`---------'|    `n    `n     ,--~~  __-/~~--'_____.
       |~~~~~~~~~|---~---/=|___________|=\---~-----~-----| .--~~  |  .__|     |
     -[|.--_. ===|#####|-| |@@@@|+-+@@@| |]=###|/-++++-[| ||||___+_.  | `===='-.
     -[|'==~'    |#####|-| |@@@@|+-+@@@| |]=###|\-++++-[| ||||~~~+~'  | ,====.-'
       |_________|---u---\=|~~~~~~~~~~~|=/---u-----u-----| '--__  |  '~~|     |
        \       /=-   `    |,---------.|      `     `    `--__  ~~-\__--.~~~~~'
----=:===\     /           |`---------'|                      ~~--_/~~--'
      --<:\___/--          |===========|
                           |-----------|
                           |___________|");
      System.Console.WriteLine();
      System.Console.Write("You awake from Hypersleep suddenly. You can see the lights are flickering above you,and the air smells foul. All the other pods are already open, you must be the last to wake. How is this when you are the captain? You can tell something must be wrong. You need to get to the bridge located at the northern most part of the ship to see what is wrong with the electrical system and check your course. You get dressed and notice your nametag says: ");
      string name = Console.ReadLine();
      Player newPlayer = new Player(name);
      GameService gameService = new GameService(newPlayer);
      gameService.StartGame();

    }
  }
}
