   M S B A M L   `   `   `  ����  QMS.GUI    QMS.GUI.WaitingRoomScreen   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   P LPrism.Wpf, Version=6.3.0.0, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   
     � Title$   ��5   	   $	��600��6   $	��600��6"    � WindowStyle$
 None=�61    � 
ResizeMode$ NoResize=�6D    � WindowStartupLocation$ CenterScreen=�6Z   .��5      � -   5         �Name$ WaitingHeaderMenu��5      ��5	   
   �� 5
       �� EndPoint$ 0.5,1&�6"    � MappingMode$ RelativeToBoundingBox=�63    �� 
StartPoint$ 0.5,0&�6W   .��5      �  � Color$
 Blue��6   	 � Offset$	 0Q�6,   � 5      $ 
BlueViolet��6   5      
 � ColumnDefinitions
 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $��32 �6   �� 5      �� 5      $��189.6 �6   .+�5   
   � $��2��6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�63   $��20��6d   $��3,0,0,0q�6p   ��6N   Z� .+�5      �� $&�� ../Content/Resources/favicon.ico��6   5      5      �� 5   
   $��1��6   $��2��6'   $��0��68   !  Prism.Regions.RegionManager   
RegionName$ QueueDetailsSection��6H   $��0,0,0,189.2q�6}   �� 5   
   $��1��6   $ StreamSection��6(   $��0,0,0,189.2q�6W   $��2��6l   $��1��6}   �� 5    
   $��2��6   $ CurrentTicketSection��6&   $��0,0.4,0,0.2q�6\   $��2��6q   5!      5"      