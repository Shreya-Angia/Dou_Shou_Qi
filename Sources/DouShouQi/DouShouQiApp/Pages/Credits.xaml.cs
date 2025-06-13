namespace DouShouQiApp.Pages;
using DouShouQiModel;
using Stubs;

public partial class Credits : ContentPage
{
    public Credits()
	{
		InitializeComponent();
    }

    public static readonly TeamMember Shreya = new TeamMember
    {
        Image = "profil_shreya.jpg",
        Name = "Shreya",
        Description = "Développeuse principale, interface & logique de jeu" 
    };

    public static readonly TeamMember Anae = new TeamMember
    {
        Image = "profil_anae.jpg",
        Name = "Anae",
        Description = "Responsable design, UX & animations"
    };

    public static readonly TeamMember Gregoire = new TeamMember
    {
        Image = "profil_gregoire.jpg",
        Name = "Grégoire",
        Description = "Développement IA & gestion du plateau"
    };

    public static readonly TeamMember Enzo = new TeamMember
    {
        Image = "profil_enzo.jpg",
        Name = "Enzo",
        Description = "Gestion de projet & intégration MAUI"
    };

}