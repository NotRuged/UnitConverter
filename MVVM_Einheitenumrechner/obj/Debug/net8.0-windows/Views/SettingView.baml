   M S B A M L   `   `   `  ����  MVVM_Einheitenumrechner/    )MVVM_Einheitenumrechner.Views.SettingView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      ���#FEF3E1  � 	Resources  5
         
BeschInput �� 
TargetType �� Property �� Value  BeschButtonStyle �� 
TargetType �� CornerRadius��%�  &	        &	 0    �� 5   
   " ��6#   .�5      ��  � �� �#F0F7F66+   �� 5       � �� �#70C1B36,   �� 5       � ��$ 1q�60   �� 5       � ��$ 6q�6(   �� 5       � ��$ 14(�6)   �� 5       � ��$ 0,0,0,15q�6'   �� 5       � ��$ 30��6'   5      �� 5   
   " ��6)   .�5      ��  � ��$ SemiBold"�6+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$ 0q�60   �� 5       � ��$ 40��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 10��5    !   ._�5!      �� $��Center=�6/   $��Center=�5"   /   5#      5$      5&      5'      5(      .��5*      � $��20q�6    � RowDefinitions 5+   
   �� 5,      $
��Auto �6   �� 5-      $��* �6   .+�51   
   ]� $��0��6   $��False��6!   $��0,0,0,10q�67   .+�52      �� -   5        $��← Back��52      $	��100��55      $��35��56      ���#E0F2F1���#00695C$
��Bold"�59      ���#B2DFDB#��[ 5;       � ToolTip$$ Zurück zur vorherigen Ansicht��5<      ��54      �� *BackCommand+5<      5=      �� 5@   
   $��0��6   $��Center=�6"   $��0,0,0,20q�6?   .+�5A      �� $��⚙ Einstellungen��6   $��36(�5B      $
��Bold"�5C      $��Center=�5D      $, &Hier können Sie Einstellungen ändern��5E      ��5F      �� 5G       �� 
StartPoint$	 0,0&�6*   	 �� EndPoint$		 1,0&�6;   .��5H      � 
 � Color$
 #70C1B3��6'    � Offset$	 0.0Q�67   � 5I      $
 #43A047��6'   $	 0.5Q�67   � 5J      $
 #00838F��6'   $	 1.0Q�67   5K      5M      �� 5N      $��4��6   $	��280��6#   $��0,10,0,0q�6/   ��5O      �� 5P      $	 0,0&�6*   $		 1,0&�6;   .��5Q      � $
 #70C1B3��6'   $	 0.0Q�67   � 5R      $
 #43A047��6'   $	 0.5Q�67   � 5S      $
 #00838F��6'   $	 1.0Q�67   5T      5V      5W      �� 5Z   
   $��1��6    �� Padding$ 25q�5[      $ 12��5\      
����������#DDD$��1q�5_      $	��400��5`      $��Center=�5a      $	��Top=�5b      $	��355��6)   $, &Formular zur Eingabe der Einstellungen��5c      ._�5d      �� .+�5f      �� $��	Kategorie��6   $
��Bold"�6-   $��15(�6?   $��0,0,0,5q�6M   $* $Bezeichnung der Umrechnungskategorie��6^   �� 5g      #��[  68   $F @Geben Sie hier den Namen der Kategorie ein, z.B. Länge, Gewicht��6\   ��6   �� *CategoryName+6\   �� 5i      $��Einheit��6   $
��Bold"�6+   $��15(�6=   $��0,0,0,5q�6K   $ Bezeichnung der Einheit��6\   �� 5j      #��[  60   $6 0Geben Sie die Einheit ein, z.B. Meter, Kilogramm��6T   ��6   �� *Unit+6T   �� 5l      $��Faktor��6   $
��Bold"�6*   $��15(�6<   $��0,0,0,5q�6J   $( "Umrechnungsfaktor zur Basiseinheit��6[   �� 5m      #��[  62   $R LFaktor zur Umrechnung in die Basiseinheit, z.B. 1000 für Kilometer zu Meter��6V   ��6   �� *Factor+6V   �� 5o      $��SemiBold"�5q      $��13(�5r      $��0,5,0,15q�5s      $, &Informationen und Hinweise zur Eingabe��5t      ��5o      �� *MessageText+��5p      �� *MessageColor+5t      �� 5v      $��💾 Speichern��6   ���#70C1B3
�������$��15(�5z      $	��150��5{      $��Right=�5|      #��[ 5}      $. (Speichert die eingegebenen Einstellungen��5~      ��5w      �� *SaveCommand+5~      5      5�      5�      5�      