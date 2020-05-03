using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

namespace MovieBrowser
{
    public class Movie_VM : VM_Base
    {
        private string _Name = "default";
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged();
            }
        }

        private int _RY = -1;
        public int RY
        {
            get { return _RY; }
            set
            {
                _RY = value;
                NotifyPropertyChanged();
            }
        }

        private double _Rating = -1;
        public double Rating
        {
            get { return _Rating; }
            set
            {
                _Rating = value;
                NotifyPropertyChanged();
            }
        }

        private string _ExtraInfo = "";
        public string ExtraInfo
        {
            get { return _ExtraInfo; }
            set
            {
                _ExtraInfo = value;
                NotifyPropertyChanged();
            }
        }


        private ObservableCollection<m_Genre> _Genres = new ObservableCollection<m_Genre>()
        {
            m_Genre.Action,
            m_Genre.Comedy
        };
        public ObservableCollection<m_Genre> Genres
        {
            get { return _Genres; }
            set
            {
                _Genres = value;
                NotifyPropertyChanged();
            }
        }


        private ObservableCollection<m_Tag> _Tags = new ObservableCollection<m_Tag>()
        {
            m_Tag.Beautiful,
            m_Tag.Classic
        };
        public ObservableCollection<m_Tag> Tags
        {
            get { return _Tags; }
            set
            {
                _Tags = value;
                NotifyPropertyChanged();
            }
        }
    }


    public enum m_Genre : byte
    {
        Action = 0,
        Detective = 1,
        Romantic = 2,
        Drama = 3,
        Horror = 4,
        Comedy = 5
    }

    public enum m_Tag : byte
    {
        Аниме = 0,
        Мультфильм = 1,
        For_Kids = 5,
        Classic = 3,
        History = 4,
        Soviet = 2,
        Beautiful = 6
    }
}
