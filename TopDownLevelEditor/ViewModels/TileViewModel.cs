using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    [Serializable]
    public class TileViewModel : NotifyBase, ITile
    {
        //private int _Id;
        public int Id
        {
            get => (PaletteGridY * 32) + PaletteGridX;
            //set
            //{
            //    PaletteGridX = value % 32;
            //    PaletteGridY = value / 32;
            //    NotifyPropertyChanged();
            //    NotifyPropertyChanged(nameof(PaletteGridX));
            //    NotifyPropertyChanged(nameof(PaletteGridY));
            //}
        }

        private int _PaletteGridX;
        public int PaletteGridX
        {
            get => _PaletteGridX;
            set
            {
                _PaletteGridX = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Id));
                NotifyPropertyChanged(nameof(TileViewBox));
            }
        }

        private int _PaletteGridY;
        public int PaletteGridY
        {
            get => _PaletteGridY;
            set
            {
                _PaletteGridY = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Id));
                NotifyPropertyChanged(nameof(TileViewBox));
            }
        }

        //private double _RoomDrawPositionX = 0.0f;
        public double RoomDrawPositionX
        {
            get => RoomGridX * TileWidth;
            //set { _RoomDrawPositionX = value; NotifyPropertyChanged(); }
        }

        //private double _RoomDrawPositionY = 0.0f;
        public double RoomDrawPositionY
        {
            get => RoomGridY * TileHeight;
            //set { _RoomDrawPositionY = value; NotifyPropertyChanged(); }
        }

        //private Rect _TileViewBox = new Rect(0, 0, 0.03125f, 0.03125f);
        public Rect TileViewBox
        {
            //TODO: This should be changed so that it's calculated as:
            //      0,0,(PaletteGridX * (TileWidth/PaletteImageWidth)),(PaletteGridY * (TileHeight/PaletteImageHeight))
            get => new Rect(PaletteGridX * 0.03125f, PaletteGridY * 0.03125f, 0.03125f, 0.03125f);
            //set { _TileViewBox = value; NotifyPropertyChanged(); }
        }

        private string _TilePaletteImageSource;
        public string TilePaletteImageSource
        {
            get => _TilePaletteImageSource;
            set { _TilePaletteImageSource = value; NotifyPropertyChanged(); }
        }

        private double _TileWidth = 0.0f;
        public double TileWidth
        {
            get => _TileWidth;
            set
            {
                _TileWidth = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(RoomDrawPositionX));
            }
        }

        private double _TileHeight = 0.0f;
        public double TileHeight
        {
            get => _TileHeight;
            set
            {
                _TileHeight = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(RoomDrawPositionY));
            }
        }

        private int _RoomGridX = 0;
        public int RoomGridX
        {
            get => _RoomGridX;
            set
            {
                _RoomGridX = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(RoomDrawPositionX));
            }
        }

        private int _RoomGridY = 0;
        public int RoomGridY
        {
            get => _RoomGridY;
            set
            {
                _RoomGridY = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(RoomDrawPositionY));
            }
        }

        public TileViewModel(int tileX, int tileY)
        {
            PaletteGridX = tileX;
            PaletteGridY = tileY;
            //TileViewBox = new Rect(tileX * 0.03125f, tileY * 0.03125f, 0.03125f, 0.03125f);
        }

        //public TileViewModel(SerializationInfo info, StreamingContext context)
        //{
        //    PaletteGridX = info.GetValue<int>(nameof(PaletteGridX));
        //    PaletteGridY = info.GetValue<int>(nameof(PaletteGridY));

        //    RoomGridX = info.GetValue<int>(nameof(RoomGridY));
        //    RoomGridY = info.GetValue<int>(nameof(RoomGridX));

        //    TileWidth = info.GetValue<double>(nameof(TileWidth));
        //    TileHeight = info.GetValue<double>(nameof(TileHeight));

        //    TilePaletteImageSource = info.GetValue<string>(nameof(TilePaletteImageSource));
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue(nameof(PaletteGridX), _PaletteGridX);
        //    info.AddValue(nameof(PaletteGridY), _PaletteGridY);

        //    info.AddValue(nameof(RoomGridX), _RoomGridX);
        //    info.AddValue(nameof(RoomGridY), _RoomGridY);

        //    info.AddValue(nameof(TileWidth), _TileWidth);
        //    info.AddValue(nameof(TileHeight), _TileHeight);

        //    info.AddValue(nameof(TilePaletteImageSource), _TilePaletteImageSource);
        //}
    }
}
