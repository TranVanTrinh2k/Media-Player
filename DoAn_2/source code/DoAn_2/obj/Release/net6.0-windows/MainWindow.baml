   M S B A M L   `   `   `  ���� A  =DoAn_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DoAn_2DoAn_2      DoAn_2.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:DoAn_2     -     � Title$  Media Player��$	��800��$
��1234�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Icon$ /ip14plus.jpg�� � 	Resources  ,  (CustomSliderBackgroundSolidColorBrushKey �� Color , (CustomSliderForegroundSolidColorBrushKey + 'CustomSliderHighlightSolidColorBrushKey ' #CustomSliderThumbControlTemplateKey �� 
TargetType � UseLayoutRounding ��Name	 �� RadiusX
 �� RadiusY �� Stretch �� Path �� RelativeSource �� Triggers P� Property P� Value �� 
TargetName �� Value �� Property " CustomSliderControlTemplateKey ��Name J� SnapsToDevicePixels � RowDefinitions ]�Name ]� Thumb n�Name � OverridesDefaultStyle �� Value �� Binding �� ElementName  CustomSliderStyleKey �� 
TargetType  ButtonStyle  TextBlockStyle��%�  &	        &	      &	 *     &	 ?     7  7  7   7  &	 �    7  7   7  7  &	 �    7  &	 b    &	     �� $ 	#55ffffff���� $ 	#ff1e91ea���� $ 	#ffefefef���� " n�.R�� $��Center=�$��Center=�$
 True��.+��� $ gripRectangle��$��8��$	 3��$
 3��$��0��$
 Fill=�8��  $
 True������ $ Height� �� *TemplatedParent+ P�  �@n�
IsDragging$
 true��. ��� $ gripRectangle��8   � ��P�  � {�$ false��. ��� $ gripRectangle��8   � ��P�  �@J�IsKeyboardFocused$
 true��. ��� $ gripRectangle��8   � ���� " ��.R��� $ border��
������ $
 True��._��  �� #��z��.+�]� $
��True��.+��� $ PART_SelectionRange��$��3��$
}�True��$y�Visible=�._��� $��4 0 -4 0q�$	 1��$
 1��8��  �� $��3��$
}�True��$y�Visible=�._��� $ PART_NegativeSelection��$��8 0 4 0q�$	 1��$
 1��8�� ]� $ 
PART_Track�� n� $ Thumb��8�� $
 True��$��Center=�$��8��#��z��$|�False�� �� $
 True�� �� $ Thumb��$ 
IsDragging�.a��� $ PART_NegativeSelection��8   � ���� " ��.���  � ��8   ��  � ��$ Center=���  � ��$ 26����  � ��$ 26����  � ��$ 26����  � ��
 ���� ��  � ��
 ���� ��  �@��IsMoveToPointEnabled$
 True����  �@��AutoToolTipPlacement$
 None=���  �@��IsSnapToTickEnabled$
 True����  �@��TickFrequency$
 0.01Q���  �@�SmallChange$
 0.01Q���  �@�LargeChange$
 0.01Q���  �@�Minimum$ 0Q���  �@��SelectionStart �� $ Minimum� �� *Self+��  �@��SelectionEnd �� $ Value� �� *Self+��  �@��IsPressAndHoldEnabled$ false���� " ��.���  � ��$ 30����  � ��$ 30����  � ��
 ���� ��  � ��
 ���� ��  � ��
 �   ���  �@�Cursor$
 Hand���� " ��.���  � ��$ 40����  � ��
 �������  � ��$ TimeNewRoman)���  �@��TextAlignment$ Center=�.���  � ColumnDefinitions �� $	��350 ��� .+�� -   $��GridMenu��$��0��.+��� 
���   �$��1q�
���   �._�� $��30��$	��Top=�.+��� -   $��playlistPath��$�� ��$��60��$��30��$y�Visible=��� $	��300��$	��0 5q�$��16(��� #��[ $	��2 2q�.���� $��Icon/icons8-search-40.png��� $��0,3,0,0q�.+��� -   $��HistoryList��$y�Hidden=�$��	0,-3,0,29q����� .R�� .+�A� $��48��$��48��$��Center=�����   �� ImageSource  �� *Image_Video+�� $
��5 10q�.+��� $
��Wrap=�$��18(�$��Black"�$��10,0,0,0q�
���   ����� *	MusicName+�� $��10(�$��Italic$�$��10,5,0,0q����� *Time+�� $y�Hidden=����� *
FilePath+�� -   $��ListViewVideo��$��	0,-3,0,27q�$��Visible=����� .R�� .+�A� $��48��$��48��$��Center=�����   �� *Image_Video+�� $
��5 10q�.+��� $
��Wrap=�$��18(�$��Black"�$��10,0,0,0q����	#FF0A07F7���� *	MusicName+�� $��10(�$��Italic$�$��10,5,0,0q����� *Time+�� $y�Hidden=����� *
FilePath+� 
���   �$��Bottom=�.+��� -   $��btn_add��#��[ ! � ToolTip$	! Add��.���� $ ��Icon/icons8-add-new-40.png���� -   $��
btn_remove��#��[ $! Remove��.���� $��Icon/icons8-minus-40.png���� -   $��
ButtonSave��#��[ $! SaveList��.���� $��Icon/icons8-save-40.png���� -	   $��
ButtonLoad��#��[ $! 
UploadList��.���� $��Icon/icons8-upload-40.png���� -
   $��CreateListBtn��#��[ $! Create new playlist��.���� $"��Icon/icons8-wish-list-40.png���� -   $��
HisroryBtn��#��[ $! Show history��.���� $��Icon/icons8-search-40.png���� -   " ��Name$" playlistName��$��default.txt��
���  ��$	��167��# �� TextAlignment$# Right=�� $��1��.+�r� -   $��player��$ r� LoadedBehavior$$ Manual=�$��0,4,0,62q��� 
���   �$	��100��$��Bottom=�.+�� .+��� -   $��
currentPos��$
��Left=�$��Center=�#��[ $
��0:00���� -   $��totalPos��$��Right=�$��Center=�#��[ $
��5:00���� -   $��progressSlider��#���  $	��850��% � Cursor$
% Hand��& � Value$& 0Q�' � Minimum$' 0Q�( � Maximum$( 50Q��� $��Bottom=�$��50��$��0 10 0 0q�$��Center=�.+��� -   $��Shuffle��) �� Right$	) 300��#��[ $! Shuffle��.���� $ ��Icon/icons8-shuffle-40.png���� -   $��	Prev_Skip��$	) 200��#��[ $! Go back 10s��.���� $$��Icon/icons8-go-to-start-40.png���� -   $��
ButtonPrev��$	) 100��#��[ $! Previous��.���� $&�� Icon/icons8-skip-to-start-40.png���� -   $��
ButtonPlay��$) 0��#��[ $
! Play��.���� $��Icon/icons8-start-40.png���� -   $��
ButtonNext��$
) -100��#��[ $
! Next��.���� $��Icon/icons8-end-40.png���� -   $��	Next_Skip��$
) -200��#��[ $! 	Go To 10s��.���� $"��Icon/icons8-go-to-end-40.png���� $
) -300��#��[ $! Audio��.���� $��Icon/icons8-audio-40.png���� -   $��VolumeLabel��$# Center=�
�������$��40��$��30��* �� Left$	* 343��
+ �� Top$+ -7��, J� RenderTransformOrigin$, 	0.44,1.31&�$��Center=�$	��Top=��� -   $��Volume��$	��110��$	* 305��$+ 6��#���  $	& 100Q�$	( 100Q�