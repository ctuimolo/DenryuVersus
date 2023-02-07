using Godot;

namespace Players
{
  public enum PlayerColors
  {
    Red,
    Blue,
  }

  public enum PlayerNumbers
  {
    One,
    Two,
  }

  public struct PlayerInputs
  {
    public string Up;
    public string Down;
    public string Left;
    public string Right;
    public string Button1;
    public string Button2;
    public string Button3;
    public string Button4;
    public string ButtonStart;
  }

  public partial class Player : CharacterBody2D
  {
    public static PlayerInputs PlayerOneInputMap = new PlayerInputs()
    {
      Up = "player1_up",
      Down = "player1_down",
      Left = "player1_left",
      Right = "player1_right",
      Button1 = "player1_button1",
      Button2 = "player1_button2",
      Button3 = "player1_button3",
      Button4 = "player1_button4",
      ButtonStart = "player1_start"
    };

    public static PlayerInputs PlayerTwoInputMap = new PlayerInputs()
    {
      Up = "player2_up",
      Down = "player2_down",
      Left = "player2_left",
      Right = "player2_right",
      Button1 = "player2_button1",
      Button2 = "player2_button2",
      Button3 = "player2_button3",
      Button4 = "player2_button4",
      ButtonStart = "player2_start"
    };
  }
}
