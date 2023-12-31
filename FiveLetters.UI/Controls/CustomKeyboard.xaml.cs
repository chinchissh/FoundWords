﻿using FiveLetters.UI.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FiveLetters.UI.Controls;

public partial class CustomKeyboard : UserControl
{
    public static readonly RoutedEvent LetterClickEvent = EventManager.RegisterRoutedEvent(
        name: nameof(LetterClick),
        routingStrategy: RoutingStrategy.Bubble,
        handlerType: typeof(RoutedEventHandler),
        ownerType: typeof(CustomKeyboard));

    public static readonly RoutedEvent RemoveClickEvent = EventManager.RegisterRoutedEvent(
        name: nameof(RemoveClick),
        routingStrategy: RoutingStrategy.Bubble,
        handlerType: typeof(RoutedEventHandler),
        ownerType: typeof(CustomKeyboard));

    public static readonly RoutedEvent EnterClickEvent = EventManager.RegisterRoutedEvent(
        name: nameof(EnterClick),
        routingStrategy: RoutingStrategy.Bubble,
        handlerType: typeof(RoutedEventHandler),
        ownerType: typeof(CustomKeyboard));

    public static readonly DependencyProperty RemoveButtonEnabledProperty
        = DependencyProperty.Register(nameof(RemoveButtonEnabled), typeof(bool), typeof(CustomKeyboard), new PropertyMetadata
        {
            DefaultValue = true,
            PropertyChangedCallback = (d, e) => ((CustomKeyboard)d).RemoveBtn.IsEnabled = (bool)e.NewValue
        });

    public static readonly DependencyProperty EnterButtonEnabledProperty
        = DependencyProperty.Register(nameof(EnterButtonEnabled), typeof(bool), typeof(CustomKeyboard), new PropertyMetadata
        {
            DefaultValue = true,
            PropertyChangedCallback = (d, e) => ((CustomKeyboard)d).EnterBtn.IsEnabled = (bool)e.NewValue
        });


    public event RoutedEventHandler LetterClick
    {
        add { AddHandler(LetterClickEvent, value); }
        remove { RemoveHandler(LetterClickEvent, value); }
    }

    public event RoutedEventHandler RemoveClick
    {
        add { AddHandler(RemoveClickEvent, value); }
        remove { RemoveHandler(RemoveClickEvent, value); }
    }

    public event RoutedEventHandler EnterClick
    {
        add { AddHandler(EnterClickEvent, value); }
        remove { RemoveHandler(EnterClickEvent, value); }
    }


    public static readonly DependencyProperty LangModeProperty
        = DependencyProperty.Register(nameof(LangMode), typeof(LangMode), typeof(CustomKeyboard),
            new()
            {
                DefaultValue = LangMode.Rus,
                PropertyChangedCallback = new PropertyChangedCallback(LangModeChanged)
            });

    public LangMode LangMode
    {
        get => (LangMode)GetValue(LangModeProperty);
        set => SetValue(LangModeProperty, value);
    }

    public bool RemoveButtonEnabled
    {
        get => (bool)GetValue(RemoveButtonEnabledProperty);
        set => SetValue(RemoveButtonEnabledProperty, value);
    }

    public bool EnterButtonEnabled
    {
        get => (bool)GetValue(EnterButtonEnabledProperty);
        set => SetValue(EnterButtonEnabledProperty, value);
    }

    public CustomKeyboard()
    {
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
        => UpdateKeyboard();

    private static void LangModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var keyboard = (CustomKeyboard)d;
        keyboard.UpdateKeyboard();
    }

    private void UpdateKeyboard()
    {
        var letters = GetLetters();
        Rows.ItemsSource = letters;
    }

    private char[][] GetLetters()
    {
        if (LangMode == LangMode.Rus)
        {
            return new char[][]
            {
                "ЙЦУКЕЁНГШЩЗХЪ".Select(x=>x).ToArray(),
                "ФЫВАПРОЛДЖЭ".Select(x=>x).ToArray(),
                "ЯЧСМИТЬБЮ".Select(x=>x).ToArray()
            };
        }
        else
        {
            return new char[][]
            {
                "QWERTYUIOP".Select(x=>x).ToArray(),
                "ASDFGHJKL".Select(x=>x).ToArray(),
                "ZXCVBNM".Select(x=>x).ToArray()
            };
        }
    }

    private void RaiseCustomRoutedEvent(char letter)
    {
        LetterRoutedEventArgs routedEventArgs = new(letter)
        {
            RoutedEvent = LetterClickEvent
        };

        RaiseEvent(routedEventArgs);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var btn = sender as Button;
        var letter = (char)btn.Content;

        RaiseCustomRoutedEvent(letter);
    }

    private void RemoveBtn_Click(object sender, RoutedEventArgs e)
    {
        var routedEvent = new RoutedEventArgs
        {
            RoutedEvent = RemoveClickEvent
        };

        RaiseEvent(routedEvent);
    }

    private void EnterBtn_Click(object sender, RoutedEventArgs e)
    {
        var routedEvent = new RoutedEventArgs
        {
            RoutedEvent = EnterClickEvent
        };

        RaiseEvent(routedEvent);
    }
}
