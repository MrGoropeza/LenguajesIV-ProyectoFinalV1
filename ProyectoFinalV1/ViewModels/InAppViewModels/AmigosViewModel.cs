using GalaSoft.MvvmLight.Command;
using ProyectoFinalV1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ProyectoFinalV1.ViewModels.InAppViewModels
{
    public class AmigosViewModel : BaseViewModel
    {
        #region atributos
        private string busqueda;
        private string busquedaString;

        private ObservableCollection<OpinionModel> opinionesCollection;


        private int itemTreshold;
        private bool isRefreshing;
        private bool isVisible;
        #endregion
        #region propiedades
        public string BusquedaStringTxt
        {
            get { return this.busquedaString; }
            set { SetValue(ref this.busquedaString, value); }
        }
        public string BusquedaTxt
        {
            get { return this.busqueda; }
            set { SetValue(ref this.busqueda, value); }
        }
        public ObservableCollection<OpinionModel> SearchItems
        {
            get { return this.opinionesCollection; }
            set { SetValue(ref this.opinionesCollection, value); }
        }
        public int ItemTreshold
        {
            get { return this.itemTreshold; }
            set { SetValue(ref this.itemTreshold, value); }
        }
        public bool IsRefreshingTxt
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public bool IsVisibleTxt
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }
        #endregion
        #region Comandos
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(RefreshMethod);
            }
        }
        public ICommand BuscarCommand
        {
            get
            {
                return new RelayCommand(BuscarMethod);
            }
        }
        public ICommand ActualizarBusquedaCommand
        {
            get
            {
                return new RelayCommand(ActualizarBusquedaMethod);
            }
        }
        public ICommand BusquedaSelectCommand
        {
            get
            {
                return new RelayCommand(BusquedaSelectMethod);
            }
        }
        #endregion
        #region Metodos
        private void RefreshMethod()
        {
            throw new NotImplementedException();
        }
        private void BuscarMethod()
        {
            throw new NotImplementedException();
        }
        private void ActualizarBusquedaMethod()
        {
            throw new NotImplementedException();
        }
        private void BusquedaSelectMethod()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Constructor
        public AmigosViewModel()
        {

        }
        #endregion
    }
}
