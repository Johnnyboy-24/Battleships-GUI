   M S B A M L   `   `   `  ����I  EBattleships V2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Battleships_V2Battleships_V2  5   	       Battleships_V2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Battleships_V2     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   $	��200��6)    � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��
Battle.png��6   64   �� 5   
   $��1��6   $��Center=�6   .��6:   Please select GameMode:6S   	� 5   
   -   5        $��4q�5      $��2��6!   .��6L   Player vs. Player6_   	� 5   
   -   5        $��4q�5      $��3��6!   .��6L   ?=Player vs. Computer (Please select a difficulty fo Player 2!)6�   	� 5   
   -   5        $��4q�5      $��4��6!   .��6L   ECComputer vs. Computer (Please selct a difficulty for both players!)6�   �� 5   
   $��10q�6   $��5��6   .��6*   Please enter Player Names:6F   � 5   
   $��6��6    5      �� 5      $
��Auto �6    �� 5       $
��Auto �6     5"      �� 5#      $	��100 �6#   �� 5$      $��* �6$   $	��200��6.   �� 5%      $	��300 �6#   .+�5'      �� $��0��6   $��0��6!   .��62   
Player 16<   �� 5(      -   5         ��Name$ Name1��5(      $��0��6%   $��1��62   .��6h   
Player 16r   �� 5)      $��1��6   $��0��6!   .��62   
Player 26<   �� 5*      -   5        $ Name2��5*      $��1��6%   $��1��62   .��6h   
Player 26r   �� 5+      -   5         ��Name$ Difficulty_player_1��5+      $��0��64   $��2��6A    �� Text$$ Please set Computer Difficulty��6Q    �� 
IsEditable . .@�5,      �� $��Easier��6   �� 5-      $��Harder��6   5.      �� 5/      -   5        $ Difficulty_player_2��5/      $��1��64   $��2��6A   $$ Please set Computer Difficulty��6Q    . 
�������.@�50      �� $��Easier��6   �� 51      $��Harder��6   52      55      �� 56   
   -   5        $��30q�56      $��7��6   $	��200��6*   .��6L   Fertig6T   57      58      