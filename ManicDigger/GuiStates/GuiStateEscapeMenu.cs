﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ManicDigger
{
    partial class ManicDiggerGameWindow
    {
        private void EscapeMenuStart()
        {
            guistate = GuiState.EscapeMenu;
            menustate = new MenuState();
            FreeMouse = true;
        }
        bool escapemenuOptions = false;
        private void EscapeMenuMouse()
        {
            int textheight = 50;
            int starty = ycenter(3 * textheight);
            if (mouse_current.Y >= starty && mouse_current.Y < starty + 3 * textheight)
            {
                menustate.selected = (mouse_current.Y - starty) / textheight;
            }
            else
            {
                menustate.selected = -1;
            }
            if (mouseleftclick && menustate.selected != -1)
            {
                EscapeMenuAction();
                mouseleftclick = false;
            }
        }
        void EscapeMenuAction()
        {
            if (!escapemenuOptions) { EscapeMenuActionMain(); }
            else { EscapeMenuActionOptions(); }
        }
        private void EscapeMenuActionOptions()
        {
            switch (menustate.selected)
            {
                case 0:
                    currentshadows.ShadowsFull = !currentshadows.ShadowsFull;
                    terrain.UpdateAllTiles();
                    break;
                case 1:
                    textdrawer.NewFont = !textdrawer.NewFont;
                    cachedTextTextures.Clear();
                    break;
                case 2:
                    escapemenuOptions = false;
                    break;
                default:
                    throw new Exception();
            }
        }
        private void EscapeMenuActionMain()
        {
            switch (menustate.selected)
            {
                case 0:
                    GuiStateBackToGame();
                    break;
                case 1:
                    escapemenuOptions = true;
                    break;
                case 2:
                    exit = true;
                    this.Exit();
                    break;
                default:
                    throw new Exception();
            }
        }
        void EscapeMenuDraw()
        {
            List<string> items = new List<string>();
            if (!escapemenuOptions)
            {
                items.Add("Return to game");
                items.Add("Options");
                items.Add("Exit");
            }
            else
            {
                items.Add("Shadows: " + (currentshadows.ShadowsFull ? "ON" : "OFF"));
                items.Add("Font: " + (textdrawer.NewFont ? "2" : "1"));
                items.Add("Return to main menu");
            }
            int textheight = 50;
            int fontsize = 20;
            int starty = ycenter(3 * textheight);
            if (guistate == GuiState.EscapeMenu)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    string s = items[i];
                    Draw2dText(s, xcenter(TextSize(s, fontsize).Width), starty + textheight * i, fontsize, menustate.selected == i ? Color.Red : Color.White);
                }
            }
        }
        private void EscapeMenuKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            int menuelements = 3;
            if (e.Key == OpenTK.Input.Key.Escape)
            {
                escapemenuOptions = false;
                GuiStateBackToGame();
            }
            if (e.Key == OpenTK.Input.Key.Up)
            {
                menustate.selected--;
                menustate.selected = Math.Max(0, menustate.selected);
            }
            if (e.Key == OpenTK.Input.Key.Down)
            {
                menustate.selected++;
                menustate.selected = Math.Min(menuelements - 1, menustate.selected);
            }
            if (menustate.selected != -1
                && (e.Key == OpenTK.Input.Key.Enter || e.Key == OpenTK.Input.Key.KeypadEnter))
            {
                EscapeMenuAction();
            }
        }
    }
}
