using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MovieBrowser
{
    public class TopVM : VM_Base
    {
        public TopVM()
        {
            Movies = TempClass.populate();
            SelectedMovie = Movies.ElementAt(0);

            _CVS = new CollectionViewSource
            {
                Source = Movies
            };
            _CVS.Filter += _SearchFilter.MySearchFilter;
        }


        public ObservableCollection<Movie_VM> Movies { get; set; } = new ObservableCollection<Movie_VM>();


        private CollectionViewSource _CVS;
        public ICollectionView SCO { get { return _CVS.View; } }


        private Movie_VM _SelectedMovie;
        public Movie_VM SelectedMovie
        {
            get { return _SelectedMovie; }
            set
            {
                _SelectedMovie = value;
                NotifyPropertyChanged();
            }
        }


        private bool _CanEdit = false;
        public bool CanEdit
        {
            get { return _CanEdit; }
            set
            {
                _CanEdit = value;
                NotifyPropertyChanged();
            }
        }


        private readonly SearchFilter _SearchFilter = new SearchFilter();
        public string SearchText
        {
            get { return _SearchFilter.SearchText; }
            set
            {
                _SearchFilter.SearchText = value;
                NotifyPropertyChanged();
                SCO.Refresh();
            }
        }


        private readonly SortFilter _SortFilter = new SortFilter();
        private RelayCommand _SortColumn_CMD;
        public RelayCommand SortColumn_CMD
        {
            get
            {
                /// These are basically the same
                /// If field is null -> set it
                return _SortColumn_CMD ?? (_SortColumn_CMD = new RelayCommand(columnName =>
                    {
                        SortDescription SD = _SortFilter.SortView((string)columnName);
                        _CVS.SortDescriptions.Clear();
                        _CVS.SortDescriptions.Add(SD);
                    }));


                //if (_SortColumn_CMD != null)
                //    return _SortColumn_CMD;
                //else
                //{
                //    //Action<object> action = obj =>
                //    //{
                //    //    SortDescription SD = SF.SortView((string)obj);
                //    //    _CVS.SortDescriptions.Clear();
                //    //    _CVS.SortDescriptions.Add(SD);
                //    //};
                //    void action(object obj)
                //    {
                //        SortDescription SD = SF.SortView((string)obj);
                //        _CVS.SortDescriptions.Clear();
                //        _CVS.SortDescriptions.Add(SD);
                //    }
                //    _SortColumn_CMD = new RelayCommand(action);
                //    return _SortColumn_CMD;
                //}
            }
        }


        private ICommand _StartEdit;
        public ICommand StartEdit
        {
            get
            {
                return _StartEdit ?? (_StartEdit = new RelayCommand((obj) =>
                {
                    CanEdit = !CanEdit;
                }));
            }
        }
    }

    /// <summary>
    /// This class contains logic for
    /// sorting view based on parametres
    /// </summary>
    public class SortFilter
    {
        public string LastSortedColumn { get; private set; }
        public bool SortUp { get; private set; } = false;

        public SortDescription SortView(string Column)
        {
            bool sortUp = (Column == LastSortedColumn) ? !SortUp : false;
            LastSortedColumn = Column;
            SortUp = sortUp;

            ListSortDirection LSD = sortUp ? ListSortDirection.Ascending : ListSortDirection.Descending;
            return new SortDescription(Column, LSD);
        }
    }

    /// <summary>
    /// This class contains logic for
    /// filtering view based on search parametres
    /// </summary>
    public class SearchFilter
    {
        public string SearchText;
        public void MySearchFilter(object sender, FilterEventArgs e)
        {
            var movie = (Movie_VM)e.Item;
            string movieName = ((Movie_VM)e.Item).Name;

            e.Accepted = string.IsNullOrEmpty(SearchText) ||
                movie.Name.Contains(SearchText) ||
                (movie.RY.ToString() == SearchText);
        }
    }


    public static class TempClass
    {
        public static ObservableCollection<Movie_VM> populate()
        {
            var outp = new ObservableCollection<Movie_VM>()
            {
                new Movie_VM(),
                new Movie_VM()
                {
                    Name = "m1",
                    RY = 100,
                    Rating = 3,
                    Tags = new ObservableCollection<m_Tag>() { m_Tag.History },
                    Genres = new ObservableCollection<m_Genre>() { m_Genre.Romantic }
                },
                new Movie_VM()
                {
                    Name = "m2",
                    RY = 52,
                    Rating = 4,
                    Tags = new ObservableCollection<m_Tag>() { m_Tag.For_Kids },
                    Genres = new ObservableCollection<m_Genre>() { m_Genre.Romantic }
                },
                new Movie_VM()
                {
                    Name = "m3",
                    RY = 77,
                    Rating = 5,
                    Tags = new ObservableCollection<m_Tag>() { m_Tag.Мультфильм },
                    Genres = new ObservableCollection<m_Genre>() { m_Genre.Romantic }
                }
            };

            return outp;
        }
    }
}
