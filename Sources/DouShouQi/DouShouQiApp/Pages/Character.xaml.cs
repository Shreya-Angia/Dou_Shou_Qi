/***************************************************************************
* Character.xaml.cs
* -------------------------------------------------------------------------
* Project       : DouShouQi Mythology
* Author        : ANGIA Shreya
* Date          : 29/05/2025
* Description   : Displays and allows the navigation through the list of 
*                 the different gods and goddesses.           
* -------------------------------------------------------------------------
***************************************************************************/ 


using System.Xml;
using DouShouQiModel;
using Stubs;

namespace DouShouQiApp.Pages;


public partial class Character : ContentPage 
{
    //List of the different gods
    private List<Stubs.Character> Characters { get; set; }
    // Index of the different gods in the list
    private int _currentIndex = 0;

    public Character()
    {
        InitializeComponent();
        Characters = Stubs.Character.GetAll();
        DisplayCurrentCharacter(); 
    } 
    /// <summary>
    /// Displays the current character
    /// </summary>
    private void DisplayCurrentCharacter()
    {
        if (Characters == null || Characters.Count == 0)
            return;

        var character = Characters[_currentIndex]; 

        CharacterName.Text = character.Name;
        CharacterCharacteristic.Text = character.Characteristic; 
        CharacterDescription.Text = character.Description; 
        CharacterImage.Source = character.Image ;   
    }

    /// <summary>
    /// Displays the next character when clicking on the right button 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnNextClicked(object sender, EventArgs e)
    {
        if (Characters == null || Characters.Count == 0)
            return;

        // Increment the index to show the next character.
        // Adding Characters.Count and using modulo ensures circular navigation
        _currentIndex = (_currentIndex + 1) % Characters.Count;
        DisplayCurrentCharacter();
    } 

    /// <summary>
    /// Displays the previous character when clicking on the left button
    /// </summary>
    /// <param name="sender"></param>   
    /// <param name="e"></param>    
    private void OnPreviousClicked(object sender, EventArgs e)
    {
        if (Characters == null || Characters.Count == 0)
            return;

        // Decrement the index to show the previous character. 
        // Adding Characters.Count and using modulo ensures circular navigation 
        _currentIndex = (_currentIndex - 1 + Characters.Count) % Characters.Count;

        DisplayCurrentCharacter(); 
    }
}