using System;
using System.Globalization;
using DouShouQiModel;
using MauiControls = Microsoft.Maui.Controls;

namespace DouShouQiApp.Converter
{
    public class ColorConverter : MauiControls.IValueConverter
    {
        public static App? CurrentApp => App.Current as App;
        public static Game? CurrentGame => CurrentApp?.CurrentGame;

        public string? Item { get; set; }
        public string? Item2 { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Item == "Cell")
            {
                System.Diagnostics.Debug.WriteLine($"Convert called with Item = {Item}, Item2 = {Item2}, Value = {value}, ValueType = {value?.GetType().Name}");

                if (value is DouShouQiModel.Cell cell)
                {
                    System.Diagnostics.Debug.WriteLine($"Cell.Type = {cell.Type}");

                    string imageName = cell.Type switch
                    {
                        CellType.Water => "water.png",
                        CellType.Normal => "grass.png",
                        CellType.House => "house.png",
                        CellType.Trap => "trap.png",
                        _ => "default.png"
                    };

                    System.Diagnostics.Debug.WriteLine($"Returning image: {imageName}");
                    return MauiControls.ImageSource.FromFile(imageName);

                }
            }

            else if (Item == "Piece")
            {
                if (Item2 == "Border")
                {
                    if (value is Piece piece)
                    {
                        return piece.Team switch
                        {
                            Team.Greek => MauiControls.Application.Current?.Resources.TryGetValue("Grec-LIGHT", out var color) == true ? color : Colors.Blue,
                            Team.Roman => MauiControls.Application.Current?.Resources.TryGetValue("Roman-LIGHT", out var color) == true ? color : Colors.Green,
                            _ => Colors.Transparent
                        };
                    }
                }
                else if (Item2 == "Background")
                {
                    if (value is Piece piece && (targetType == typeof(MauiControls.ImageSource) || targetType == typeof(object)))
                    {
                        string? imageName = piece.PieceName switch
                        {
                            "Heracles" => "heracles.png",
                            "Aphrodite" => "aphrodite.png",
                            "Hermès" => "hermes.png",
                            "Héphaïstos" => "hephaistos.png",
                            "Athena" => "athena.png",
                            "Hades" => "hades.png",
                            "Poseidon" => "poseidon.png",
                            "Zeus" => "zeus.png",

                            "Hercules" => "hercule.png",
                            "Vénus" => "venus.png",
                            "Mercury" => "mercure.png",
                            "Vulcan" => "vulcain.png",
                            "Minerva" => "minerve.png",
                            "Pluto" => "pluton.png",
                            "Neptune" => "neptune.png",
                            "Jupiter" => "jupiter.png",
                            _ => null
                        };

                        return ImageSource.FromFile(imageName);
                    }
                }
                else if (Item2 == "BackgroundColor")
                {
                    if (value is Piece piece)
                    {
                        if (piece.PieceName == "canMove")
                        {
                            return MauiControls.Application.Current?.Resources.TryGetValue("CanMove", out var color) == true ? color : Colors.LightGreen;
                        }
                        else if (piece.IsSelected)
                        {
                            return MauiControls.Application.Current?.Resources.TryGetValue("SelectedPiece", out var color) == true ? color : Colors.Yellow;
                        }
                        else
                        {
                            return MauiControls.Application.Current?.Resources.TryGetValue("DefaultPiece", out var color) == true ? color : Colors.Transparent;
                        }
                    }
                }
            }
            if (Item == "Pseudo")
            { 
                if (Item2 == "P1")
                {
                    return CurrentGame?.Player1?.Name ?? string.Empty;
                }
                else if (Item2 == "P2")
                {
                    return CurrentGame?.Player2?.Name ?? string.Empty;
                }
            }
            return null!;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}