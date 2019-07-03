using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Commands for cozmo
public class Command : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private string ty = ""; //Type string

    private string t = ""; //text string

    private float f1; // float 1
    private float f2; // float 2

    private int ti = 0; // times



    public Command(string type) // command constructor with type and no inputs
    {
        ty = type;


    }//command()

    public Command(string type, string text) // command constructor with string input
    {

        ty = type;

        t = text;




    }//speakCommand()

    public Command(string type, float float1) // command constructor with 1 float input
    {
        ty = type;

        f1 = float1;


    }//turnCommand()

    public Command(string type, float float1, float float2) // command constructor with 2 float inputs
    {
        ty = type;

        f1 = float1;
        f2 = float2;

    }//forwardCommand()


    public Command(string type, int times) // command constructor with one integer input
    {
        ty = type;

        ti = times;


    }//forCommand()

    public string getOutput(int indents) // gets python command with nessisary amount of indents 
    {

        string output = "";
        for (int o = 0; o <= indents - 1; o++) // adds indents
        {

            output = output + " \t";

        }//for

        switch (ty) // adds python code to output with nessisary variables
        {
            case "speak":
                output = output + "\t" + "robot.say_text(\"" + t + "\").wait_for_completed()";
                return output;

            case "forward":
                output = output + "\t" + "robot.drive_straight(distance_mm(" + f1 + "), speed_mmps(" + f2 + ")).wait_for_completed()";
                return output;

            case "turn":
                output = output + "\t" + "robot.turn_in_place(degrees("+f1+")).wait_for_completed()";
                return output;

            case "liftUp":
                output = output + "\t" + "robot.move_lift(5).wait_for_completed()";
                return output;

            case "liftDown":
                output = output + "\t" + "robot.move_lift(-5).wait_for_completed()";
                return output;

            case "for":
                output = output + "\t" + "for n in range(0," + ti + "):";
                return output;

            default:
                output = output + "";
                return output;

        }//switch

    }//getOutput

    public string getType() // gets type string
    {
        return ty;
    }
}
