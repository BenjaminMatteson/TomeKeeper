using TomeKeeper.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TomeKeeper.ViewModels
{
    public class SpellDetailsViewModel : INotifyPropertyChanged
    {
        [Required]
        private SpellDetails _spellDetails;

        public SpellDetailsViewModel()
        {
            _spellDetails = new SpellDetails
            {
                Index = string.Empty,
                Name = string.Empty,
                Desc = new List<string>(),
                HigherLevel = new List<string>(),
                Range = string.Empty,
                Components = new List<string>(),
                Material = string.Empty,
                Ritual = false,
                Duration = string.Empty,
                Concentration = false,
                CastingTime = string.Empty,
                Level = 0,
                School = null,
                Classes = new List<ApiReference>(),
                Subclasses = new List<ApiReference>()
            };
        }

        public SpellDetailsViewModel(SpellDetails spellDetails)
        {
            _spellDetails = spellDetails;
            OnPropertyChanged(string.Empty);

        }

        #region Properties

        public string Index
        {
            get => _spellDetails.Index;
            set
            {
                if (_spellDetails.Index == value) return;
                _spellDetails.Index = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _spellDetails.Name.ToUpper();
            set
            {
                if (_spellDetails.Name == value) return;
                _spellDetails.Name = value;
                OnPropertyChanged();
            }
        }

        public List<string> Desc
        {
            get => _spellDetails.Desc;
            set
            {
                if (_spellDetails.Desc == value) return;
                _spellDetails.Desc = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => string.Join("\n", _spellDetails.Desc);
            set
            {
                var descList = value.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (_spellDetails.Desc.SequenceEqual(descList)) return;
                _spellDetails.Desc = descList;
                OnPropertyChanged();
            }
        }

        public List<string> HigherLevel
        {
            get => _spellDetails.HigherLevel ?? new();
            set
            {
                if (_spellDetails.HigherLevel == value) return;
                _spellDetails.HigherLevel = value;
                OnPropertyChanged();
            }
        }
        public string HigherLevelString
        {
            get
            {
                if (HigherLevel == null || HigherLevel.Count == 0)
                {
                    return string.Empty;
                }
                return " " + string.Join(" ", HigherLevel.Select(h => h.Trim()));
            }
        }
        public bool HasHigherLevel => HigherLevel != null && HigherLevel.Count > 0;

        public string Range
        {
            get => " " + _spellDetails.Range;
            set
            {
                if (_spellDetails.Range == value) return;
                _spellDetails.Range = value;
                OnPropertyChanged();
            }
        }

        public List<string> Components
        {
            get => _spellDetails.Components ?? new();
            set
            {
                if (_spellDetails.Components == value) return;
                _spellDetails.Components = value;
                OnPropertyChanged();
            }
        }
        public string ComponentsString
        {
            get
            {
                if (Components == null || Components.Count == 0)
                {
                    return string.Empty;
                }

                if (Material != null && Material.Length > 0)
                {
                    return $" {string.Join(", ", Components.Select(c => c))} ({Material.ToLower()})";
                }

                return $" {string.Join(", ", Components.Select(c => c))}";
            }
        }

        public string? Material
        {
            get => _spellDetails.Material;
            set
            {
                if (_spellDetails.Material == value) return;
                _spellDetails.Material = value;
                OnPropertyChanged();
            }
        }

        public bool Ritual
        {
            get => _spellDetails.Ritual;
            set
            {
                if (_spellDetails.Ritual == value) return;
                _spellDetails.Ritual = value;
                OnPropertyChanged();
            }
        }

        public string Duration
        {
            get => _spellDetails.Duration ?? string.Empty;
            set
            {
                if (_spellDetails.Duration == value) return;
                _spellDetails.Duration = value;
                OnPropertyChanged();
            }
        }
        public string DurationString
        {
            get
            {
                if (_spellDetails.Concentration)
                    return " Concentration, " + _spellDetails.Duration.ToLower();

                return _spellDetails.Duration; 
            }

            set
            {
                if (_spellDetails.Duration == value) return;
                _spellDetails.Duration = value;
                OnPropertyChanged();
            }
        }

        public bool Concentration
        {
            get => _spellDetails.Concentration;
            set
            {
                if (_spellDetails.Concentration == value) return;
                _spellDetails.Concentration = value;
                OnPropertyChanged();
            }
        }

        public string CastingTime
        {
            get => " " + _spellDetails.CastingTime ?? string.Empty;
            set
            {
                if (_spellDetails.CastingTime == value) return;
                _spellDetails.CastingTime = value;
                OnPropertyChanged();
            }
        }
        public string CastingTimeString
        {
            get
            {
                if(_spellDetails.Ritual)
                    return " " + _spellDetails.CastingTime?.Trim() + " or Ritual";

                return " " + _spellDetails.CastingTime;
            }
        }

        public int Level
        {
            get => _spellDetails.Level;
            set
            {
                if (_spellDetails.Level == value) return;
                _spellDetails.Level = value;
                OnPropertyChanged();
            }
        }

        public ApiReference? School
        {
            get => _spellDetails.School;
            set
            {
                if (_spellDetails.School == value) return;
                _spellDetails.School = value;
                OnPropertyChanged();
            }
        }

        public List<ApiReference> Classes
        {
            get => _spellDetails.Classes;
            set
            {
                if (_spellDetails.Classes == value) return;
                _spellDetails.Classes = value;
                OnPropertyChanged();
            }
        }

        public List<ApiReference> Subclasses
        {
            get => _spellDetails.Subclasses;
            set
            {
                if (_spellDetails.Subclasses == value) return;
                _spellDetails.Subclasses = value;
                OnPropertyChanged();
            }
        }

        public string SchoolLevelClassesString
        {
            get
            {
                var schoolText = School != null ? $"{School.Name}" : string.Empty;
                var levelText = Level == 0 ? "Cantrip" : $"Level {Level}";
                var classesText = Classes != null && Classes.Count > 0
                    ? $" ({string.Join(", ", Classes.Select(c => c.Name))})"
                    : string.Empty;

                if (Level == 0)
                {
                    return $"{schoolText} Cantrip {classesText}";
                }

                return $"{levelText} {schoolText} {classesText}";
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
