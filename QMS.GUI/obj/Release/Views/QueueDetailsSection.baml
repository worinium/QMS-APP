   M S B A M L   `   `   `  ����   QMS.GUI'    !QMS.GUI.Views.QueueDetailsSection   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -   .��� -     �Name$  TokenSideMenuGrid������  �� EndPoint$ 0.5,1&� � MappingMode$ RelativeToBoundingBox=� �� 
StartPoint$ 0.5,0&�.���  � Color$
 Blue�� � Offset$ 0Q�� $ 
BlueViolet�� � RowDefinitions �� $
��Auto ��� $��1* ��� $��2* �.+�&   System.Windows.Controls.DataGrid  -     Name$ DataGridQueueDetails��   DataGridCellStyle   	CellStyle# [  $	��300��
����+��	   VerticalGridLinesBrush
	 �����
   CanUserResizeRows
 .     CanUserResizeColumns .     CanUserReorderColumns .     CanUserSortColumns .  ��Z�    HorizontalGridLinesBrush Z�    ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value �	#FF8A2BA2��  � ��
 �������  � ��
 �������  � ��$ Center=���  � ��$ 1,1,1,1q���  � ��$ 18(�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Service��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 42*    
IsReadOnly .    FontSize$ 18Q�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ServiceTypeDescription+  $	 12*  . $ 18Q� �� *CurrentToken+   HeaderTemplate �� .R��� $��Last Ticket��$
��Wrap=�$��Center=� �� TextAlignment$ Center=�  $	 13*  . $ 18Q� �� *
SeatNumber+ �� .R��� $��Seat Number��$
��Wrap=�$��Center=�$ Center=�  $	 12*  . $ 18Q� �� *	NextToken+ �� .R��� $��Next Ticket��$
��Wrap=�$��Center=�$ Center=�  $	 21*  . $ 18Q� �� *	QueueSize+ �� .R��� $��Customers in Queue��$
��Wrap=�$��Center=�$ Center=�� $��2��$��Bottom=�.+��� $��Center=�.+��� -    ��Name$ timerLbl��
�������$��Center=�$��Center=�$��Tahoma)�$��35(�$��Center=�$��58��$��Next Token in: 20 seconds���� -   $ timeLbl��
�������$��Center=�$��Center=�$��Tahoma)�$��35(�$��Center=�$��48��$��00:00:00���� -   $ dateLbl��
�������$��Center=�$��Center=�$��Arial)�$��25(�$��Center=�$��48��$��
00.00.2000��