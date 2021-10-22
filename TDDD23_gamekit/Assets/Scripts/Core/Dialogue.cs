using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script stores every dialogue conversation in a public Dictionary.*/

public class Dialogue : MonoBehaviour
{

    public Dictionary<string, string[]> dialogue = new Dictionary<string, string[]>();

    void Start()
    {
        //Door
        dialogue.Add("LockedDoorA", new string[] {
            "A large door...",
            "Looks like it has a key hole!"
        });


        dialogue.Add("LockedDoorB", new string[] {
            "Key used!"
        });

        //NPC
        dialogue.Add("CharacterA", new string[] {
            "Hi there!",
            "I'm an NPC! This conversation is called 'npcA'...",
            "If you go and find me 80 coins, my dialogue will move on to 'npcB'!",
            "Feel free to edit my dialogue in the 'Dialogue.cs' file!",
            "To keep it simple, you can also ask me one, and only one, question...",
            "...Like you just did! And I'll just move on to the next sentence.",
            "I'll answer that question, but it won't change much about the game!",
            "You can always tweak the 'DialogueBox.cs' script to add more functionality!"
        });

        dialogue.Add("CharacterAChoice1", new string[] {
            "",
            "",
            "Let me go find some coins!",
        });

        dialogue.Add("CharacterAChoice2", new string[] {
            "",
            "",
            "What else can you do?"
        });

        dialogue.Add("CharacterB", new string[] {
            "Hey! You found 80 coins! That means 'npcB' is now being used inside 'Dialogue.cs'!",
            "After my dialogue completes, I'll take 80 coins, or however many you specify in the inspector...",
            "And I'll also give you a new ability!",
            "In this case, how about a generic DOWNWARD SMASH? Simply attack while pressing down in mid-air!"
        });

        //=============Introduction level dialog=============

        // Check if Mac or Windows
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            dialogue.Add("BookA", new string[] {
            "Hey! You're finally up..",
            "You must have a lot of questions, about why you're here and who I am.",
            "But do not worry, everything will make sense in time.",
            "I can assure you that this isn't a dream, so don't go do anything stupid.",
            "We can start by making sure you know the basics.",
            "You are able to draw and I will try to guess what it is.\nI will make the object appear and you will be able to use it.",
            "I might guess wrong but hey, its your drawing skills at fault, not mine.",
            "Before doing any further, is it okay for me to hear what you're saying?\n(Grant permission to enable microphone)",
            "Awesome, now try saying \"car\" loudly and see what happends.",
            "Looking good.",
            "I'll be leaving it up to you to explore the rest.",
            "You are ready to go on a long journey to find out who you are.",
            "I'll be by your side to help you along the way.",
            "You got this, lets go!"
        });

            dialogue.Add("BookB", new string[] {
            });

            dialogue.Add("BookAChoice1", new string[] {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "Yes, its okay!",
            });


            dialogue.Add("BookAChoice2", new string[] {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "No, just let me leave!",
            });
        }
        else
        {
            dialogue.Add("BookA", new string[] {
            "Hey! You're finally up..",
            "You must have a lot of questions, about why you're here and who I am.",
            "But do not worry, everything will make sense in time.",
            "I can assure you that this isn't a dream, so don't go do anything stupid.",
            "We can start by making sure you know the basics.",
            "You are able to draw and I will try to guess what it is.\nI will make the object appear and you will be able to use it.",
            "I might guess wrong but hey, its your drawing skills at fault, not mine.",
            "It happends to be that I can't hear your very well today so you will have to spell it out if you want to.",
            "You can do this by using the magnifying icon on the lower right",
            "I'll be leaving it up to you to explore the rest.",
            "You are ready to go on a long journey to find out who you are.",
            "I'll be by your side to help you along the way.",
            "You got this, lets go!" });
        }
            

        
        



























    }
}
