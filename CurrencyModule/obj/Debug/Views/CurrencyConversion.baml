   M S B A M L   `   `   `  ����L  HCurrencyConverter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:CurrencyConverter.ControlsCurrencyConverter.Controls  5      S OToggleSwitch, Version=1.1.2.0, Culture=neutral, PublicKeyToken=8637099990568f75A0clr-namespace:ToggleSwitch;assembly=ToggleSwitchToggleSwitch 5      K*clr-namespace:CurrencyConverter.ConvertersCurrencyConverter.Converters  5      A%clr-namespace:CurrencyConverter.ViewsCurrencyConverter.Views  5      0    *CurrencyConverter.Views.CurrencyConversion   5      5Controls(clr-namespace:CurrencyConverter.Controls  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      IToggleSwitch0clr-namespace:ToggleSwitch;assembly=ToggleSwitch     5      Bopconverter*clr-namespace:CurrencyConverter.Converters     5      7local%clr-namespace:CurrencyConverter.Views     5        � 	Resources  5      F   @CurrencyConverter.Converters.BooleanToOpositeVisibilityConverter   oppositeConverter��%   &	          5   
   6:   5      .��5      �� $��1q�6   $	��570��6!   $	��785��6.   
�������
������� J� Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5      ._�5   
   � $	��570��6   $	��785��6   $��Center=�6(   $��Center=�6E    � ColumnDefinitions 5      �� 5      $	��400 �6#   �� 5      $	��400 �6#    � RowDefinitions 5      �� 5      $��66 �6    �� 5      $	��174 �6    �� 5      $��* �6    �� 5      $��50 �6    .+�5      1   +CurrencyConverter.Controls.CurrencySelector  -   5          Name$ CurrencySelectorFrom��5   (   $
��Left=�6F   $��Center=�6a   $��0,0,10,0q�6|     5      -   5        $ CurrencySelectorTo��5   (   $
��Left=�6D   $��1��6_   $��Center=�6o   .   (CurrencyConverter.Controls.CurrencyInput  5      -   5          Name$ 	InputFrom��5   %   $��1��68     5       -   5        $ InputTo��5    %   $��1��66   $��1��6C   �� 5!      $��66��6   $��0,0,0,1q�6!   
�������$��2��6S   $��10,0,0,0q�6g   �� 5"      $��Center=�6   $	��Top=�6T   $��0,44,0,0q�6l    � Cursor$
 Hand��6~   $��42��6�   $��42��6�   $��2��6�   $��2��6�   ��62   �� *RevertCommand+  5#       �� 
TargetType �� Property	 �� Value��%_   '��       �� 5$      " ��6   .�5%      ��  �@��CornerRadius$	 15��69   5&      5'      ��5(      �� 5)      
 �� ImageSource$
 
revert.png��6!    j� Stretch$ UniformToFill=�6:   5+      �� 5,      $	��270��6   $��0,4,0,0q�6"   
��� ���$��2��6U   $��2��6b   $��0q�6v   ._�5-      �  5.      �� 5/      $	��200 �6+   �� 50      $	��700 �6+    52      �� 53      $��80 �6(   66   �� 54      $��* �6(   65   .+�56      �� $��20,0,0,0q�6   $	��Top=�6/   $��2��6G   $	��750��6X   $
��Left=�6e   $��65��6�   $��2��6�   
�������$��0,0,0,1q�6�   ._�57      �� $'��!Exchange Rates (major currencies)��6$   $
��Left=�6M   $��Center=�6h   $��32(�6�   $
��Bold"�6�   6�   58      �� 59      $��1��6   $��0,0,1,0q�6*   $	��160��6D   
�������._�5:      �� .+�5;      �  5<   "   �� 5=   &   $
��auto �67   6F   �� 5>   &   $
��auto �67   6F   .+�5@   "   ��  �� Orientation$ 
Horizontal=�6-   $��0,30,0,0q�6F   .+�5A   &   �� $
��15,0q�60   $��Center=�6>   $��Inverse��6Y   $��16(�6h   6x   )  #ToggleSwitch.HorizontalToggleSwitch  5B   &   $��1��5C   %   $��0��5D   %   $��Center=�5F   %   $
��Left=�5G   %   #  ToggleSwitch.ToggleSwitchBase   	IsChecked 5E   %   �� *InverseCurrencyCompare+ �� Mode$ TwoWay=�   UncheckedContent 5H   *   � 5I   .   .+�5J   2   F� ����        �@x}@x}	
������� �� Stretch$ UniformToFill=�6Y   $��6��6q   F� 5K   2   ����    @x}�@x}    	
�������$ UniformToFill=�6Y   $��6��6q   5L   /      CheckedContent 5N   *   F� 5O   .   
�������$ UniformToFill=�6@   F����K@�� Ao    � Ao��р�O �6�    @���    @��ѠK@�	6X   5Q   '   5R   #   5S      �� 5T      $ 
Horizontal=�6)   $��	15,30,0,0q�6B   .+�5U   "   �� $��0,0,10,0q�6M   $��24��6_   ��6(   �� *FromCurrency.Icon+6m   �� 5V   "   $��5,0,0,5q�6,   $��1��6=   $��32(�6F   y�6T   �� *InverseCurrencyCompare+ �� 	Converter# [  5W   [   �� 5X   "   $��32(�6S   
����  �$
��Bold"�6x   ��6,   �� *FromCurrency.Currency+6�   5Y      5[      5\      -   'CurrencyConverter.Controls.CurrencyGrid  5]      -   5          Name$ MajorCurrencies��5]   ,   $��1��6E   $
��Left=�6U   $��10q�6p   $��1��6|   $	��Top=�6�   5^      5_      �� 5`      $��3��6   $��2��6&   $��15q�6:   $ 
Horizontal=�6F   .+�5a      �� $��Currency Rates as ��6   68   �� 5b      $
��Bold"�6   ��6.   �� *
LoadDate+ �� StringFormat$ {0:dd-MMM-yyyy}��6i   5c      �� 5d      $��3��6   $��1��6&   $��15,15,25,15q�66   $ 
Horizontal=�6K   $��Right=�6d   .+�5e      �� $��Right=�6   $��Source: ��69   6K   �� 5f      �� 5g      -   5        $,��&https://api.exchangeratesapi.io/latest?�5g       .�6�   exchangeratesapi.io5i      5j      5k      5m      5n      5o      