   M S B A M L   `   `   `  ���� W  SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35iFclr-namespace:System.Windows.Media.Animation;assembly=PresentationCoreSystem.Windows.Media.Animation   HUK-Zeiterfassung#   HUK_Zeiterfassung.ClockWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      \	animationFclr-namespace:System.Windows.Media.Animation;assembly=PresentationCore      -     � Title$  ClockWindow�� � WindowStyle$
 None=� � AllowsTransparency . 
������  � 
ResizeMode$ NoResize=�.��� $	��230������  �� ElementName$ ClockProject�� �� Path$ ActualWidth� � RowDefinitions �� $��23 ��� $��23 ��� -    ��Name$ gridrow��$��23 �.+��� -    ��Name$ clockBorder��$��1q�$��3��$��0���� -   	 ��Name$	 ClockProject��
�������$��0��$
��Left=�$��Center=�$��20(�$��0,0,0,0q�
 �� Padding$
 	10,0,10,0q��� -   $	 ClockTextBlock��$��1��$��20(�
�������$
��Left=�$��5,0,0,0q�$��Center=�$
 5,0,10,8q��� -   $	 ReminderTextBlock��$��2��$��IFS Zeiten eintragen!��$y�	Collapsed=�$��20(�
�������$
��Left=�$��5,0,0,0q�$��Center=�$
 	5,0,10,10q� � ContextMenu �� .@�m� -   $#��IFS Zeiten wurden eingetragen��