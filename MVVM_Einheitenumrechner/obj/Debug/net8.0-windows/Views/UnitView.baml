   M S B A M L   `   `   `  ����R  NMVVM_Einheitenumrechner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:MVVM_Einheitenumrechner.ViewModel!MVVM_Einheitenumrechner.ViewModel  5      ,    &MVVM_Einheitenumrechner.Views.TestView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      >vm/clr-namespace:MVVM_Einheitenumrechner.ViewModel     5      $	��600��5      $	��900��5	      ���#FEF3E1$��20q�5        � DataContext  5      5   /MVVM_Einheitenumrechner.ViewModel.UnitViewModel  5   
   .��5      � $��Center=�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0,0,0,20q�6"   $��Center=�64   .+�5      �� $��36(�6   $
��Bold"�5      $��Center=�5      $��0,0,0,5q�5       � ToolTip$0 *Titel der Anwendung – Einheitenumrechner��5      ��5      �� 5       �� 
StartPoint$	 0,0&�6*    �� EndPoint$	 1,0&�6;   .��5       �  � Color$ #70C1B3��6'    � Offset$	 0.0Q�67   � 5!      $ #43A047��6'   $	 0.5Q�67   � 5"      $ #00838F��6'   $	 1.0Q�67   5#      Einheitenumrechner5&      �� 5(      $��5��6   $	��300��5)      $��0,0,0,15q�5*      $��Center=�5+      $5 /Dekoratives Farbverlaufselement unter dem Titel��5,      ��5-      �� 5.      $	 0,0&�6*   $	 1,0&�6;   .��5/      � $ #70C1B3��6'   $	 0.0Q�67   � 50      $ #43A047��6'   $	 0.5Q�67   � 51      $ #00838F��6'   $	 1.0Q�67   52      54      � 57      $��Center=�6   $��	0,-10,0,0q�65   .+�58      �� -   5        $��Verlauf��58      $	��120��5:      $��40��5;      $��10q�5<      $��16(�5=      ���#455A64
������� � Cursor$
 Hand��5@      $��0q�5A      $+ %Zeigt den Verlauf der Umrechnungen an��5B      ��5C      �� 5D       �� 
TargetType" ��6    .�5E      �� 	 �� Property	 � ��
 �� Value
 �#455A646;   �� 5F      	 � ��

 �����6;   �� 5G      	 � ��$
 SemiBold"�6;   �� 5H      	 � ��$
 0q�6@   �� 5I      	 � ��$

 10,5q�68   �� 5J      	 �@J�Effect
 5K   "   3  -System.Windows.Media.Effects.DropShadowEffect  5L   &      Color$ Black��67      
BlurRadius$ 5Q�6E      ShadowDepth$ 2Q�6T      Opacity$	 0.2Q�6d   5N       �� Triggers 5O      P� 5P   "    P� Property �@J�IsMouseOver P� Value$
 True��6A   . �5Q   &   �� 	 � ��
 �#37474F6C   5R   #   P� 5S   "    � ��$
 True��6?   . �5T   &   �� 	 � ��
 �#2632386C   5U   #   5W      5Y      �� 5[      -   5        $��Settings��5[      $	��120��5]      $��40��5^      $��10q�5_      $��16(�5`      ���#00796B
�������$
 Hand��5c      $��0q�5d      $ Öffnet die Einstellungen��5f      ��5e      �� *EditSettingsCommand+��5g      �� 5h      " ��6    .�5i      �� 	 � ��
 �#00796B6;   �� 5j      	 � ��

 �����6;   �� 5k      	 � ��$
 SemiBold"�6;   �� 5l      	 � ��$
 0q�6@   �� 5m      	 � ��$

 10,5q�68   �� 5n      	 �@J�Effect
 5o   "     5p   &   $ Black��67   $ 5Q�6E   $ 2Q�6T   $	 0.2Q�6d   5r       5s      P� 5t   "    �@J�IsMouseOver$
 True��6A   . �5u   &   �� 	 � ��
 �#00695C6C   5v   #   P� 5w   "    � ��$
 True��6?   . �5x   &   �� 	 � ��
 �#004D406C   5y   #   5{      5}      5~      5      �� 5�   
   $��1��6   $	��Top=�6"   $��Center=�6:   $	��320��6W   .+�5�      �� $��Kategorie auswählen:��6   $��SemiBold"�5�      $��14(�5�      ���#555555$��0,0,0,5q�5�      $, &Wähle die Kategorie der Einheiten aus��5�      �� 5�       �� DisplayMemberPath$ CategoryName��5�      $	��320��5�      $��38��5�      $��0,0,0,15q�5�      
����������#CCCCCC$��1q�5�      $��15(�5�      $��6q�5�      $��Center=�5�      ���#333$' !Liste der verfügbaren Kategorien��5�      ��5�      �� *
Categories+ �� SelectedItem 5�      �� *SelectedCategory+5�      �� 5�      $��Ursprungseinheit:��6   $��SemiBold"�5�      $��14(�5�      ���#555555$��0,0,0,5q�5�      $9 3Wähle die Einheit, von der umgerechnet werden soll��5�      �� 5�      $
 Unit��5�      $	��320��5�      $��38��5�      $��0,0,0,15q�5�      
����������#CCCCCC$��1q�5�      $��15(�5�      $��6q�5�      $��Center=�5�      ���#333$, &Liste der möglichen Ausgangseinheiten��5�      ��5�      �� *AvailableUnits+ 5�      �� *
FromUnit+5�      �� 5�      $��Zieleinheit:��6   $��SemiBold"�5�      $��14(�5�      ���#555555$��0,0,0,5q�5�      $8 2Wähle die Einheit, in die umgerechnet werden soll��5�      �� 5�      $
 Unit��5�      $	��320��5�      $��38��5�      $��0,0,0,20q�5�      
����������#CCCCCC$��1q�5�      $��15(�5�      $��6q�5�      $��Center=�5�      ���#333$( "Liste der möglichen Zieleinheiten��5�      ��5�      �� *AvailableUnits+ 5�      �� *ToUnit+5�      �� 5�      $��Zahl:��6   $��SemiBold"�5�      $��14(�5�      ���#555555$��0,0,0,5q�5�      $3 -Gib die Zahl ein, die umgerechnet werden soll��5�      �� 5�      $	��320��5�      $��38��5�      $��0,0,0,15q�5�      $��15(�5�      $��6q�5�      
����������#CCCCCC$��1q�5�      $��Center=�5�      $��Center=�5�      $3 -Gib den Wert ein, der umgerechnet werden soll��5�      ��5�      �� *
InputValue+ �� UpdateSourceTrigger$ PropertyChanged=�5�      �� 5�      $	��320��5�      $��38��5�      $��0,0,0,15q�5�      $��15(�5�      $��6q�5�      
����������#CCCCCC$��1q�5�      $��Center=�5�      $��Center=�5�      $ Ergebnis der Umrechnung��5�       �� 
IsReadOnly$
 True��5�      ��5�      �� *Result+$ PropertyChanged=�5�      5�      5�      5�      