   M S B A M L   `   `   `  ����  MVVM_Einheitenumrechner/    )MVVM_Einheitenumrechner.Views.HistoryView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      ���#FEF3E1  � 	Resources  5
         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius��%�  &	        �� 5   
   " ��6+   .�5      ��  � ��$
 Bold"�6+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$ 0q�60   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 6��5   !   #��z��5   !   #��z��5   !   ._�5      �� $��Center=�6/   $��Center=�5   /   $
��True��5   /   5      5      5      5      5      .��5       � $��20q�6    � RowDefinitions 5!   
   �� 5"      $
��Auto �6   �� 5#      $��* �6   .+�5'   
   � $��0��6   $��0,0,0,20q�6    � ColumnDefinitions 5(      �� 5)      $
��Auto �6#   �� 5*      $��* �6#   �� 5+      $
��Auto �6#   .+�5/      �� -   5        $��← Zurück��5/       � ToolTip$ Zurück zur Startseite��51      $��90��52      $��35��53      ���#E0F2F1���#00695C$
��Bold"�56      ���#B2DFDB#��[  58      �� 5;      $��1��6   $��Center=�6)   $��Center=�6F   .+�5<      �� $��🔁 Verlauf��6   $��36(�5=      $
��Bold"�5>      $��Center=�5?      $. (Zeigt die Umrechnungsverlaufshistorie an��5@      ��5A      �� 5B      	 �� 
StartPoint$		 0,0&�6.   
 �� EndPoint$	
 1,0&�6?   .��5C      �  � Color$ #70C1B3��6+    � Offset$	 0.0Q�6;   � 5D      $ #43A047��6+   $	 0.5Q�6;   � 5E      $ #00838F��6+   $	 1.0Q�6;   5F      5H      �� 5J      $��5��6   $	��220��5K      $��0,5,0,0q�5L      $��Center=�5M      ��5N      �� 5O      $		 0,0&�6.   $	
 1,0&�6?   .��5P      � $ #70C1B3��6+   $	 0.0Q�6;   � 5Q      $ #43A047��6+   $	 0.5Q�6;   � 5R      $ #00838F��6+   $	 1.0Q�6;   5S      5U      5V      �� 5Y      $��2��6   $��/Images/StartPage.png��5Z      $��50��5[      $��50��5\      $��Right=�5]      $	��Top=�5^      $ Startseite öffnen��5_      5`      &   System.Windows.Controls.DataGrid  5c   
   $��1��6   $	��800��5d      $	��300��5e         AutoGenerateColumns$ False��5g         
IsReadOnly$
 True��5h         HeadersVisibility$ Column=�5i         GridLinesVisibility$ 
Horizontal=�5j         	RowHeight$ 35Q�5k         ColumnHeaderHeight$ 40Q�5l      $��14(�5m         AlternatingRowBackground �#E0F2F1
�������$��1q�5p      ���#B2DFDB   HorizontalGridLinesBrush �#B2DFDB$) #Liste aller bisherigen Umrechnungen��5s      ��5f      �� *HistoryEntries+   Columns 5t      0  *System.Windows.Controls.DataGridTextColumn  5u      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Zeitpunkt��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *	Timestamp+   HeaderStyle 5v      �� 5w      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5x      ��  �@�ToolTip$ Zeitpunkt der Umrechnung��68   5y      5{        5|      $ 	Kategorie��6%   $ * 6W    68   �� *
CategoryID+ 5}      �� 5~      "  6    .�5      ��  �@�ToolTip$1 +Umrechnungskategorie (z.B. Länge, Gewicht)��68   5�      5�        5�      $	 Von��6%   $ * 6O    62   �� *
FromUnit+ 5�      �� 5�      "  6    .�5�      ��  �@�ToolTip$$ Ausgangseinheit der Umrechnung��68   5�      5�        5�      $
 Nach��6%   $ * 6N    63   �� *ToUnit+ 5�      �� 5�      "  6    .�5�      ��  �@�ToolTip$  Zieleinheit der Umrechnung��68   5�      5�        5�      $ Eingabewert��6%   $ * 6Y    6:   �� *
InputValue+ 5�      �� 5�      "  6    .�5�      ��  �@�ToolTip$1 +Der eingegebene Wert, der umgerechnet wurde��68   5�      5�        5�      $ Ergebnis��6%   $ * 6W    67   �� *ResultValue+ 5�      �� 5�      "  6    .�5�      ��  �@�ToolTip$ Ergebnis der Umrechnung��68   5�      5�      5�      5�      5�      