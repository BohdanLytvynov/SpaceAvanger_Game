﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SpaceAvenger.Enums.FrameTypes;
using SpaceAvenger.Services.Realizations;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.ViewModels.PagesVM
{
    public class MainPageViewModel : ViewModelBase
    {       
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Commands

        public ICommand OnNewGameButtonPressed { get; }

        #endregion

        #region Ctor
        public MainPageViewModel()
        {
            

            #region Init Commands

            OnNewGameButtonPressed = new Command(
                OnNewGameButtonPressedExecute,
                CanOnNewGameButtonPressedExecute
                );

            #endregion
        }
        #endregion

        #region Methods

        #region Command Methods

        #region OnNewGameButtonPressed

        public bool CanOnNewGameButtonPressedExecute(object p)
        {
            return true;
        }

        public void OnNewGameButtonPressedExecute(object p)
        {
            PageManagerService<FrameType>.SwitchPage("levels", FrameType.MainFrame);
        }

        #endregion

        #endregion

        #endregion
    }
}
