   M S B A M L   `   `   `  ����E  AWPF_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:WPF_CrossComm_Client.MVVM_Pattern.Views'WPF_CrossComm_Client.MVVM_Pattern.Views  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5   	   6    0WPF_CrossComm_Client.MVVM_Pattern.Views.MainView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Glocal5clr-namespace:WPF_CrossComm_Client.MVVM_Pattern.Views     5   	   ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5   	   XiPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity  5   	   $��15(�5	        � Title$  MainView��5
   	   $	��700��6   $	��600��6'   $	��900��63    � WindowStartupLocation$ CenterScreen=�6B   .��5      �� $
��Auto=�6   $
��Auto=�66   .��5   
   �� .+�5      o� -   5        $��	Main_Menu��5      .@�5      m� -   5        $��File_MenuItem��5      $
��File��60   .@�5      m� -   5        $��Close_MenuItem��5      $��Close��65   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5         	EventName$ Click��6-   .  (System.Windows.Interactivity.TriggerBase   Actions. 5   "   "  Caliburn.Micro.ActionMessage     
MethodName$ CloseWindow��64   5      5      5      m� 5      -   5        $��Settings_MenuItem��5      $��Settings��64    5        5      $ Click��6)   . 5        $ Settings_Click��60   5      5      5       � 5#      -   5        $��CommunicationGrid��5#       � ColumnDefinitions 5$      �� 5%      $��* �6'   63   �� 5&      $��* �6'   63    � RowDefinitions 5(      �� 5)      $
��auto �6$   64   �� 5*      $
��auto �6$   64   �� 5+      $
��auto �6$   64   .+�5-      �� -   5        $��IP_Address_Label��5-      $��IP Address:��63   $��0��6�   $��0��6�   $
��Bold"�6�   6�   �� 5.      -   5        $��
Port_Label��5.      $��Port:��63   $��1��6�   $��0��6�   $
��Bold"�6�   6�   �� 5/      -   5        $��IP_Address_TexBox��5/      $��0��6�   $��1��6�   ��63   ��  �� Path$ 
IP_Address�	 �� Mode$	 Default=�
 �� UpdateSourceTrigger$
 PropertyChanged=�6�   �� 50      -	   5        $��Port_TextBox��50      $��1��6�   $��1��6�   ��63   �� $
 Port�$	 Default=�$
 PropertyChanged=�6�   �� 51      -
   5         ��Name$ buttonClicked��51      $��Check Connection��62   $��2��6�   $��2��6�    52        53      $ Click��6)   . 54        $ CheckConn_Clicked��60   55      57      58      �� 5<      ��6   �� *Items+ �� SelectedItem 67   �� *
ActiveItem+$	 TwoWay=� �� ContentTemplate 5=      �� 5>      .R�5?      �� $��Stretch=�6D   $��Stretch=�6g   ��.    Caliburn.Micro.View   Model 6)   �� 6�   5@      ��5B      �� 5C      .R�5D      �� ��6$   �� *DisplayName+5E      5G      �� 5J      -   5        $��Status_Label��5J      $��Status:��6/   $
��Bold"�6A   6U   �� 5K      -   5        $��Status_TextBox��5K       �� AcceptsReturn .  �� TextWrapping$
 Wrap=�5L   +   $	��200��6?   " �� VerticalScrollBarVisibility$
 Auto=�5M      $ �� HorizontalScrollBarVisibility$
 Auto=�65    �� 
IsReadOnly . ��5K   /   �� $ 
StatusText�$	 Default=�$
 PropertyChanged=�5N   &   �� 5O      -   5        $��Exception_Label��5O      $��
Exception:��6/   $
��Bold"�6D   6X   �� 5P      -   5        $��Exception_TextBox��5P       . $
 Wrap=�5Q   +   $	��200��6?   $	��900��6L   $
 Auto=�5R      $
 Auto=�65    . ��5P   /   �� $ ExceptionText�$	 Default=�$
 PropertyChanged=�.��5S   $   5U      5W      5X      5Y      