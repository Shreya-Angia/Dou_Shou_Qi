using System.ComponentModel;
using System.Runtime.CompilerServices;
using DouShouQiModel;

namespace DouShouQiApp.Pages;

/// <summary>
/// Page displaying the rules, for standard and custom rules.
/// Implements INotifyPropertyChanged for data binding.
/// </summary>
public partial class Regles : ContentPage, INotifyPropertyChanged
{
    // Event for property change notifications (data binding)
    public event PropertyChangedEventHandler? PropertyChanged;

    // Backing field for the list of rules sections currently displayed
    private List<RulesPage> _rulesSections; 

    /// <summary>
    /// List of rules sections to display (bound to the UI)
    /// </summary>
    public List<RulesPage> RulesSections
    {
        get => _rulesSections;
        set
        {
            _rulesSections = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Notifies the UI that a property value has changed
    /// </summary>
    private void OnPropertyChanged(string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // Preloaded lists of standard and custom rules
    private List<RulesPage> _standardRules = RulesPage.StandardRules();
    private List<RulesPage> _customRules = RulesPage.CustomRules();

    /// <summary>
    /// Constructor: initializes the page and sets the default rules to standard
    /// </summary>
    public Regles()
    {
        InitializeComponent();
        RulesSections = _standardRules; // default value 
        BindingContext = this;
    }

    /// <summary>
    /// displays standard rules
    /// </summary>
    private void OnClassicalClicked(object sender, EventArgs e)
    {
        RulesSections = _standardRules;
    }

    /// <summary>
    /// displays custom rules
    /// </summary>
    private void OnCustomClicked(object sender, EventArgs e)
    {
        RulesSections = _customRules;
    }

}