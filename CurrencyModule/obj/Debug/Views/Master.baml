   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      H DCurrency.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP8clr-namespace:Currency.Core.Model;assembly=Currency.CoreCurrency.Core.Model 5      L HCurrencyConverter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:CurrencyConverter.BehaviorsCurrencyConverter.Behaviors 5	      $   CurrencyConverter.Views.Master   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      1core&clr-namespace:System;assembly=mscorlib  5      Dmodel8clr-namespace:Currency.Core.Model;assembly=Currency.Core  5      5local)clr-namespace:CurrencyConverter.Behaviors  5	      Z VFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=bc9b0c37bf06c6a90mui&http://firstfloorsoftware.com/ModernUI  5
      Y	 UPrism.Unity.Wpf, Version=7.2.0.1422, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59S
 OPrism.Wpf, Version=7.2.0.1422, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/ 	 
 5      ! 
 Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources 5          BooleanToVisibilityConverter  ErrorTemplate �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value �� Self	 �� RelativeSource
 �� Path��%m  &	        &	      '�� �     �� 5   
   6'   �� 5   
   .R�5      ]� ��. .+�5      �� 
���  ��$��1q�6+   ._�5      �� 5      5      5      �� 5   
   " ��6    5      P� 5       �@=�HasError . . �5      ��  �@�ToolTip 5      �� #	 Z $)
 #(Validation.Errors)[0].ErrorContent�5      5      5       .��5!      � $��Stretch=�6    5"   
    
 myEnum S� 
MethodName S� 
ObjectType   System.Enum S� MethodParameters M� Type%  Currency.Core.Model.OrderStatus��%e   &	       S� 5$      $ 	GetValues��60   "  6G    5%      M� 5&      "  6   5(      5)      .+�5+   
   ]� $��Stretch=�6   .+�5,      ]� $	��Top=�6   $��Stretch=�6-   .+�5-      �� $��Filter by Currency: ��6   $��0,0,0,8q�68   $
��Left=�6I   �� 5.      -   5        $��
currencies��5.      $	��260��6-   $
��Left=�6:    �� IsTextSearchEnabled .  w� TextPath$ Currency��6k    �� 
IsEditable .  �� SelectedItem 5/      �� *SelectedFilterCurrency+��50      �� *AllCurrencies+��51      �� 52      .R�53      ��  �� Orientation$ 
Horizontal=�6)   .+�54   "   �� ��6,   �� *
Currency+�� 55   "   $��-��6,   �� 56   "   $
��Bold"�6E   ��6,   �� *	ISOCode+6E   57      58      .  (System.Windows.Interactivity.Interaction   Triggers 5:      /  )System.Windows.Interactivity.EventTrigger  5;         	EventName$ SelectionChanged��6)   .  (System.Windows.Interactivity.TriggerBase   Actions. 5<      6  0System.Windows.Interactivity.InvokeCommandAction     Command 64   �� *FilterCommand+5>      5?      5A      �� 5B      $	��140��6   $��10,0,0,0q�6%   $
��Left=�68   ��6N   �� *ResetCommand+��5C      �� 5D      $��Reset Filters��6$   6;   5F      �� 5G      $	��140��6   $��10,0,0,0q�6%   $��Right=�6�   $��Right=�6�   {�67   �� *IsRefreshEnabled+��6^   �� *RefreshCommand+��5H      �� 5I      $��Reload Data��6$   69   5K      5L      ]� 5M      $��Bottom=�6   
�������$��Stretch=�6G   .+�5N      �� $
��Left=�6   $��Last Rate Date: ��63   $��10,0,0,8q�6K   �� 5O      $
��Left=�6   $��10,0,0,8q�6M   ��63   �� *
RateDate+6M   �� 5P      $��Right=�6   $��Right=�63   $��10,0,0,8q�6m   ��6P   �� *DataMessage+6m   5Q      �� 5R      $��20��6   $��Bottom=�6%   6?   � 5T       � ColumnDefinitions 5U      �� 5V      $	��93* �6'   �� 5W      $	��37* �6'   �� 5X      $
��170* �6'    � RowDefinitions 5Z      �� 5[      $��* �6$   61   �� 5\      $
��Auto �6$   64   .+�5^      &   System.Windows.Controls.DataGrid     AutoGenerateColumns .  $��3��6�   ��6   �� *InUseCurrencies+ �� Mode$ TwoWay=� 6l   �� *SelectedUserCurrency+   Columns 5_      =	  7FirstFloor.ModernUI.Windows.Controls.DataGridTextColumn	  5`      ,
  &System.Windows.Controls.DataGridColumn 
  Header$ Currency��61    
  
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6U   �� *
Currency+	  5a      $ ISO Code��61    .  6U   �� *	ISOCode+	  5b      $
 Rate��61    .  6Q   �� *Rate+	  5c      $ Base Currency��61    .  6Z   �� *Base+5e      � 5f      $��1��6   
�������$��3��6<    5g      �� 5h      6-   �� 5i      6-    5k      �� 5l      $
��Auto �6(   68   �� 5m      $
��Auto �6(   68   �� 5n      $
��Auto �6(   68   �� 5o      $
��Auto �6(   68   .+�5r      �� $��Iso Code��6   $��0��6/   $��0��6<   $��4q�6L   6Y   �� 5s      $��(NEW)��6   $��0��6,   
���  ��$��0��5t   X   $��67,4,4,4q�6h   $
��Left=�6z   y�5s   J   �� *IsNew+  �� 	Converter#  [  5t   �   �� 5u      $��Currency Description��6   $��1��6;   $��0��6H   $��4q�6X   6e   �� 5v      $��Currency Rate��6   $��2��64   $��0��6A   $��4q�6Q   6^   � 5w      $��1��6   $��0��6+    5x      �� 5y      $
��Auto �6,   6<   �� 5z      $
��Auto �6,   6<   .+�5|      �� $��0��6"   $��0��62   ! =� ErrorTemplate#! [ 5~      $��4q�6X   ��5|   ?   �� $
 EditorCurrency.ISOCode�$ TwoWay=�" �� ValidatesOnDataErrors" . # �� NotifyOnValidationError# . $ �� ValidatesOnExceptions$ . .��5~   c   %   	Behaviors% 5      1  +CurrencyConverter.Behaviors.OnEnterBehavior  5�   "   .��5�   7    5�        5�   "   $ 	LostFocus��61   . 5�   &      6<   �� *UpdateRateCommand+5�   #   .��5�   6   5�      �� 5�      $@��:(input your currency code and press enter to autopopulate)��6    $��1��6e   $��0��6r   $��4q�6�   6�   5�      �� 5�      $��1��6   $��1��6.   $��4q�6v   ��6;   �� $
 EditorCurrency.Currency�$ TwoWay=�6�   �� 5�      $��1��6   $��2��6.   & �� 
IsReadOnly& . $��4q�6�   ��6M   �� $
 EditorCurrency.Rate�$ TwoWay=�6�   �� 5�      $��4��6!   $��2��6.   $ 
Horizontal=�6B   .+�5�      �� -   5        ' ��Name$
' Save��5�   n   $
��Save��6!   $	��100��6|   $��5q�6�   ��60   �� *SaveCommand+{�6P   �� *	CanSave+6�   �� 5�      -   5        $' Delete��5�   y   $��Delete��6!   $	��100��6�   $��5q�6�   ��62   �� *DeleteCommand+{�6T   �� *RecordSelected+6�   �� 5�      -   5        $' Cancel��5�   U   $��Cancel��6!   $	��100��6e   $��5q�6q   ��63   �� *CancelCommand+6~   5�      5�      5�      5�      5�      5�      