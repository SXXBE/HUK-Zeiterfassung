   M S B A M L   `   `   `  ���� \  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35N;clr-namespace:System.Windows;assembly=PresentationFrameworkSystem.Windows   HUK-Zeiterfassung   HUK_Zeiterfassung.Tracker   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      XSystemParameters;clr-namespace:System.Windows;assembly=PresentationFramework        � WindowStartupLocation$  CenterScreen=� � Title$ Zeiterfassung��$	��840��$
��Auto��$
��1460��$	��840�� � SizeToContent$ Manual=����	#FF1B1B1B � 	Resources    CalendarStyle1 �� 
TargetType&    System.Windows.Controls.Calendar �� Property �� Value �� EndPoint �� 
StartPoint	 � Color
 � Offset �� 
TargetType ��Name5   /System.Windows.Controls.Primitives.CalendarItem  Name   CalendarItemStyle  CalendarButtonStyle17   1System.Windows.Controls.Primitives.CalendarButton'   !System.Windows.VisualStateManager   VisualStateGroups%   System.Windows.VisualStateGroup   Transitions%   System.Windows.VisualTransition   GeneratedDuration   States    System.Windows.VisualState   
Storyboard g� Duration	 W� To �� TargetProperty �� 
TargetName	 �� To R� KeyTime R� Value  System.Windows.Visibility ��Name J� Opacity �� RadiusY �� RadiusX ��Name  J� IsHitTestVisible! �� Triggers" P� Property# P� Value$ �� 
TargetName  CalendarDayButtonStyle1:	   4System.Windows.Controls.Primitives.CalendarDayButton% F�Name& J� RenderTransformOrigin' �� Stretch  CalendarItemStyle1( � 	Resources) �Name  Key* �� Color+ �� CornerRadius, � Cursor- � ColumnDefinitions. � RowDefinitions/ ��Name  PreviousButtonTemplate  HeaderButtonTemplate  NextButtonTemplate  DisabledColor0 �� Value1 �� Binding2 �� RelativeSource3 �� AncestorType��%�  &	        &	 I    &	     &	     7  7  7  7  7  �� "  .���  � �� �	#FF333333��  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $	 	#FFE4EAF0��$
 0Q�� $	 	#FFECF0F4��$

 0.16Q�� $	 	#FFFCFCFD��$

 0.16Q�� $	 	#FFFFFFFF��$
 1Q���  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $	 	#FFA3AEB9��$
 0Q�� $	 	#FF8399A9��$
 0.375Q�� $	 	#FF718597��$
 0.375Q�� $	 	#FF617584��$
 1Q���  � ��$ 1q���  � �� �� "  .R��� $ 	PART_Root��$��Center=�.+�  $ PART_CalendarItem��#��z��#��z $	��337��$��346.356�����	#FF1B1B1B���	#FFFDFDFD��Z� �� "  .���  � �� �	#FFBADDE9��  � ��$ 40����  � ��$ 42����  � ��$ 10(���  � ��$ Center=���  � ��$ Center=���  � �� �� "  .R��    CommonStates��   $ 0:0:0.1D�.   
Normal��  	MouseOver��. �� .��W� $ 0D�$ .5X�$ Opacity�$ 
Background��  Pressed��. �� .��W� $ 0D�$ .5X�$ Opacity�$ 
Background��  SelectionStates��   $ 0D�.   
Unselected��  Selected��. �� .��W� $ 0D�$	 .75X�$ Opacity�$ SelectedBackground��  ActiveStates��   $ 0D�.   
Active��  Inactive��. �� .���� $ 0D�$ 	#FF777777X�$6 0(TextElement.Foreground).(SolidColorBrush.Color)�$ 
NormalText��  CalendarButtonFocusStates��   $ 0D�.   CalendarButtonFocused��. �� .��T� $ 0D�$ 
Visibility�$ CalendarButtonFocusVisual��..�i� $ 0��  Visible=�  CalendarButtonUnfocused��. �� .��T� $ 0D�$ 
Visibility�$ CalendarButtonFocusVisual��..�i� $ 0��  	Collapsed=�.+��� $ SelectedBackground��#��z��$ 0Q�$ 1��$ 1���� $ 
Background��#��z��$ 0Q�$ 1��$ 1���� $ 
NormalText�����	#FF333333#��z��$��1,0,1,1q�#��z���� $ CalendarButtonFocusVisual��  .  $ 1��$ 1�����	#FF45D6FA$y�	Collapsed=�! P� " �@J�	IsFocused# . . ��� $$ CalendarButtonFocusVisual�� � y�$ Visible=��� " 	 .���  � ��$ 5����  � ��$ 5����  � ��$ 10(���  � ��$ Center=���  � ��$ Center=���  � �� �� " 	 .R��    CommonStates��   $ 0:0:0.1D�.   
Normal��  	MouseOver��. �� .��W� $ 0D�$	 0.5X�$ Opacity�$ HighlightBackground��  Pressed��. �� .��W� $ 0D�$	 0.5X�$ Opacity�$ HighlightBackground��  Disabled��. �� .��W� $ 0D�$ 0X�$ Opacity�$ HighlightBackground��W� $ 0D�$	 .35X�$ Opacity�$ 
NormalText��  SelectionStates��   $ 0D�.   
Unselected��  Selected��. �� .��W� $ 0D�$	 .75X�$ Opacity�$ SelectedBackground��  CalendarButtonFocusStates��   $ 0D�.   CalendarButtonFocused��. �� .��T� $ 0D�$ 
Visibility�$ DayButtonFocusVisual��..�i� $ 0��  Visible=�  CalendarButtonUnfocused��. �� .��T� $ 0D�$ 
Visibility�$ DayButtonFocusVisual��..�i� $ 0��  	Collapsed=�  ActiveStates��   $ 0D�.   
Active��  Inactive��. �� .���� $ 0D�$ 	#FF777777X�$6 0(TextElement.Foreground).(SolidColorBrush.Color)�$ 
NormalText��  	DayStates��   $ 0D�.   
RegularDay��  	Today��. �� .��W� $ 0D�$ 1X�$ Opacity�$ TodayBackground���� $ 0D�$ 	#FFFFFFFFX�$6 0(TextElement.Foreground).(SolidColorBrush.Color)�$ 
NormalText��  BlackoutDayStates��   $ 0D�.   	NormalDay��  BlackoutDay��. �� .��W� $ 0D�$ .2X�$ Opacity�$ Blackout��.+��� $ TodayBackground�����	#FFF75A5A$ 0Q�$ 15��$ 15��$��47��$��35���� $ SelectedBackground�����	#FF3BF3B9$ 0Q�$ 15��$ 15���� #��z��#��z��#��z���� $ HighlightBackground�����	#FF3BF3B9$ 0Q�$ 15��$ 15���� $ 
NormalText�����#FFF$��12(�#��z��$��5,1,5,1q�#��z��F� $% Blackout��}�����5 �Z @�J� �Q2� �J� �̉� iV� �A�� �J� �W�� �J� ѷ�� #�� �>8� 7T�K�� 7T�� � ��� ћ�� 7T����G @7T��� �D� 	���	#FF000000$��Stretch=�$��3q�$ 0Q�$& 0.5,0.5&�$
' Fill=�$��Stretch=��� $ DayButtonFocusVisual��  .  $ 15��$ 15�����	#FF3BF3B9$y�	Collapsed=��� "  .���  � ��$ 0,3,0,3q���  � �� �� "  ( ���� (��       **(CalendarItem.DayTitleTemplateResourceKey+).R��� ���	#FFF7F787$
��Bold"�$	��9.5(�$��Verdana)�$��Center=�$��0,6,0,6q�$��Center=����� .R�� $) 	PART_Root�� ���� DisabledColor $* 	#A5FFFFFF��   CommonStates��.   
Normal��  Disabled��. �� .��W� $ 0D�$ 1X�$ Opacity�$ PART_DisabledVisual��.+��� #��z��#��z��#��z��$+ 1��._��� $��2q�$+ 1����Z� ._��  ���� PreviousButtonTemplate " ��.R�� $
, Hand��   CommonStates��.   
Normal��  	MouseOver��. �� .���� $ 0D�$ 	#FF73A9D8X�$* $(Shape.Fill).(SolidColorBrush.Color)�$
 path��  Disabled��. �� .��W� $ 0D�$ .5X�$" (Shape.Fill).(Brush.Opacity)�$
 path��.+��� ���	#11E5EBF1$ 15��$ 15��$ 1Q�$
' Fill=�� .+�F� $
% path��"�����5���Ѱ�5h�W��<�h�	���#FFF$
��Left=�$��10��$��11,0,0,0q�$
' Fill=�$��Center=�$��6���� NextButtonTemplate " ��.R�� $
, Hand��   CommonStates��.   
Normal��  	MouseOver��. �� .���� $ 0D�$ 	#FF73A9D8X�$* $(Shape.Fill).(SolidColorBrush.Color)�$
 path��  Disabled��. �� .��W� $ 0D�$ .5X�$" (Shape.Fill).(Brush.Opacity)�$
 path��.+��� ���	#11E5EBF1$ 15��$ 15��$ 1Q�$
' Fill=�� .+�F� $
% path��"����xT��!��xT���S�h4 	���#FFF$��Right=�$��10��$��0,0,11,0q�$
' Fill=�$��Center=�$��6���� HeaderButtonTemplate " ��.R�� $
, Hand��   CommonStates��.   
Normal��  	MouseOver��. �� .���� $ 0D�$ 	#FF73A9D8X�$6 0(TextElement.Foreground).(SolidColorBrush.Color)�$ buttonContent��  Disabled��. �� .��W� $ 0D�$ .5X�$ Opacity�$ buttonContent��.+��� $ buttonContent��#��z��#��z�����#FFF$��14(�#��z��$��1,4,1,9q�#��z��- �� $
��Auto ��� $
��Auto ��� $
��Auto �. �� $
��Auto ��� $��* �.+��� $/ PART_PreviousButton��$��0��|�.  $
��Left=�$��30��$��0��8��  $��30��$��10,3.483,0,3.483q��� $/ PART_HeaderButton��$��1��$
��Bold"�|�.  $
��10.5(�$��Center=�$��0��8�� $��Center=�$	��140���� $/ PART_NextButton��$��2��|�.  $��Right=�$��30��$��0��8�� $��30��$��0,0,10,0q�� $) PART_MonthView��$��4��$��Center=�$��6,-1,6,6q�$��1��$y�Visible=�- �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �. �� $��50 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 �� $) PART_YearView��$��3��$��Center=�$��6,-3,7,6q�$��1��$y�Hidden=�- �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �. �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $ PART_DisabledVisual��8�� $ 0Q�$ 2��$ 2��$
' Fill=�8�� $��1��$y�	Collapsed=�! P� " � {�# .  . ��� $$ PART_DisabledVisual�� � y�$ Visible=��� $
0 Year��1 �� *DisplayMode+2 �� *FindAncestor+"3  .a��� $$ PART_MonthView�� � y�$ Hidden=��� $$ PART_YearView�� � y�$ Visible=��� $0 Decade��1 �� *DisplayMode+2 �� *FindAncestor+"3  .a��� $$ PART_MonthView�� � y�$ Hidden=��� $$ PART_YearView�� � y�$ Visible=�.��� 
������ $
��Auto��$
��Auto��$	��800��$
��1400��- �� $��* ��� $��* ��� $��* �. �� $	��350 ��� $��40 ��� $��60 ��� $��* �.+�� $��0��$��2��$��
11,15,20,0q�- �� $��* ��� $��90 ��� $	��130 ��� $��75 ��� $��75 ��� $��75 ��� $��75 ��� $��75 ��� $��75 ��� $��75 �.+��� $��Projekt��
�������$��0���� $��Aktivitäts-ID��
�������$��1���� $��Kurzbezeichnung��
�������$��2���� $��Montag��
�������$��3��$��Right=��� $��Dienstag��
�������$��4��$��Right=��� $��Mittwoch��
�������$��5��$��Right=��� $��
Donnerstag��
�������$��6��$��Right=��� $��Freitag��
�������$��7��$��Right=��� $��Samstag��
�������$��8��$��Right=��� $��Sonntag��
�������$��9��$��Right=��� $��0��$��2��$��380,-10,20,10q�$��1��
������ 
������ $��Right=����� *WeeklyTimesSum+���� " ��.���  � ��$ Stretch=���  � ��$
 Auto����  �@��
IsSelected �� *Content.IsSelected+4 �� Mode$4 TwoWay=�2 �� *Self+��  � �� �� " ��.R��� ���� .R�� - �� $��75 ��� $��75 ��� $��75 ��� $��75 ��� $��75 ��� $��75 ��� $��75 �.+��� -   
�������$��0��$��Right=����� *MoSum+�� -   
�������$��1��$��Right=����� *DiSum+�� -   
�������$��2��$��Right=����� *MiSum+�� -   
�������$��3��$��Right=����� *DoSum+�� -   
�������$��4��$��Right=����� *FrSum+�� -   
�������$��5��$��Right=����� *SaSum+�� -   
�������$��6��$��Right=����� *SoSum+�� $��0��$��2��$��1��
�������$��	0,0,0,0.3q�$��
10,0,20,20q��� $��0��$��2��$��1��
�������$��	0,0,0,0.3q�$��
10,0,20,23q��� -   5 ��Name$5 ListBoxTimeEntry��$��0��$��10,40,20,10q�$��2��$��0��
����������	#FF2B2B2B���� *WeeklyProjectTimes+���� " ��.���  � ��$ Stretch=���  � ��$
 Auto����  �@��
IsSelected �� *Content.IsSelected+$4 TwoWay=�2 �� *Self+��  � �� �� " ��.R��� ���� .R�� - �� $��* ��� $��90 ��� $	��130 ��� $��75 ��� $��75 ��� $��75 ��� $��75 ��� $��75 ��� $��75 ��� $��75 �.+��� -	   $��0��$��Stretch=�
�������
������ $��0,0,1,1q����� *Name+�� -
   $��1��$��Stretch=�
�������
������ $��0,0,1,1q����� *RFC+�� -   $��2��$��Stretch=�
�������
������ $��0,0,1,1q����� *RFCID+�� -   $��3��$��right=�
�������
������ $��0,0,1,1q����� *Monday+�� -   $��4��$��right=�
�������
������ $��0,0,1,1q����� *	Tuesday+�� -   $��5��$��right=�
�������
������ $��0,0,1,1q����� *	Wednesday+�� -   $��6��$��right=�
�������
������ $��0,0,1,1q����� *
Thursday+�� -   $��7��$��right=�
�������
������ $��0,0,1,1q����� *Friday+�� -   $��8��$��right=�
�������
������ $��0,0,1,1q����� *
Saturday+�� -   $��9��$��right=�
�������
������ $��0,0,0,1q����� *Sunday+�� $��10��
�������$��	0,0,0,0.1q�$��Stretch=�  -   6  Name$6 CustomCalendar��$��2��$��2��$	��330��$��0,0,0,0q�$��Center=�$	��400��#���   7   CalendarButtonStyle#7 �  8   CalendarDayButtonStyle#8 �  # �  � $��0��$��2��$��3��- �� $��* ��� $��* ��� $��* ��� $	��100 �.+��� -   $/ TimeEntriesPageButton��$��0��$��2��$��	10,0,30,0q����	#FF0E0E0E$��16(�$��40��
�������$��Zeiteinträge��9 J� Effect9 3
  -System.Windows.Media.Effects.DropShadowEffect
  �� -   $/ ProjectsPageButton��$��1��$��2��$��	10,0,30,0q����	#FF0E0E0E$��16(�$��40��
�������$��Projekte��9 
  �� -   $/ ProjectsArchiveButton��$��2��$��2��$��	10,0,30,0q����	#FF0E0E0E$��16(�$��40��
�������$��Projektarchiv��9 
  �� -   $/ SettingsPageButton��$��3��$��2��$��	10,0,10,0q����	#FF0E0E0E$��16(�$��40��
�������.���� $��/images/gear.png��$��30��9 
  �� -   : ��Name$: CalendarWeek��$��3��$��0��$��	0,300,0,0q�$��Center=�
�������$��15(�$��Kalenderwoche: ��$��Center=� � -   ;  �Name$; MainWindowFrame��$��3��$��0��$��3��|�. $��0,0,0,10q�$
��Auto��<  � NavigationUIVisibility$< Hidden=�$��Stretch=�$��Stretch=�