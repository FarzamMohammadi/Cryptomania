   M S B A M L   `   `   `  ����H  DcryptomaniaUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:cryptomaniaUI.ViewscryptomaniaUI.Views  5      %    cryptomaniaUI.Views.ProfileView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:cryptomaniaUI.Views     5      .��5	      � 
�������.+�5
   
   �� $��Profile��6   $
��Left=�6"   $��
371,14,0,0q�6=   $	��Top=�6Q   $��20(�6i   $��Italic$�6w   �� 5   
   -   5        $��Logout��5      $
��Left=�6"   $��
618,15,0,0q�6=   $	��Top=�6Q   $��41��6�   $	��140��6�   �� 5   
   -   5        $��Purchase Currency��5      $
��Left=�6-   $��
90,337,0,0q�6H   $	��Top=�6\   $��41��6�   $	��140��6�   �� 5   
   -   5        $��
Go To Cart��5      $
��Left=�6&   $��335,337,0,0q�6A   $	��Top=�6V   $��41��6�   $	��140��6�   �� 5   
   -   5        $��New Wallet Address��5      $
��Left=�6.   $��589,337,0,0q�6I   $	��Top=�6^   $��41��6�   $	��140��6�   �� 5   
   $��Wallet Address:��6   $
��Left=�6*   $��	42,50,0,0q�6E   $	��Top=�6X   �� 5   
   -   5          ��Name$  walletAddyLbl��5      $��-��6'   $
��Left=�63   $��
133,50,0,0q�6N   $	��Top=�6b   �� 5   
   $	��Hi,��6   $
��Left=�6   $��	42,19,0,0q�69   $	��Top=�6L   �� 5   
   -   5        $  usernameLbl��5      $��-��6%   $
��Left=�61   $��	66,19,0,0q�6L   $	��Top=�6_   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ ProfileDataGrid��5      $��42,81,364,147q�6,      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ CRYPTOCURRENCY��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 250 6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *CurrencyName+6^     5      $	 QTY��6%   $ * 6X    62   �� *CurrencyQuantity+6X   5      �� 5   
   -   5        $��Refresh Wallet��5      $
��Left=�6*   $��
306,54,0,0q�6E   $	��Top=�6Y   $��22��6�   $	��130��6�   �� 5   
   $5��/To send currency please follow the steps below:��6   $
��Left=�6J   $��
449,79,0,0q�6e   $	��Top=�6y   �� 5   
   $"��1. Select currency from grid��6   $
��Left=�67   $��449,105,0,0q�6R   $	��Top=�6g   �� 5   
   $��2. Select amount��6   $
��Left=�6+   $��449,131,0,0q�6F   $	��Top=�6[   �� 5   
   $��3. Receiver Address��6   $
��Left=�6.   $��449,172,0,0q�6I   $	��Top=�6^   �� 5   
   $��4. Click Send!��6   $
��Left=�6)   $��449,223,0,0q�6D   $	��Top=�6Y   �� 5   
   -	   5         ��Name$ crypto2SendAmountTBox��5      $
��Left=�61   $��449,157,0,0q�6L    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��309��6�   �� 5    
   -
   5        $ receiverWalletAddressTBox��5       $
��Left=�65   $��449,200,0,0q�6P   $
 Wrap=�6e   $	��Top=�6y   $	��309��6�   �� 5!   
   -   5        $
��Send��5!      $
��Left=�6    $��618,260,0,0q�6;   $	��Top=�6P   $��41��6   $	��140��6�   �� 5"   
   -   5        $��Clear��5"      $
��Left=�6!   $��676,225,0,0q�6<   $	��Top=�6Q   $��21��6�   $��82��6�   5$      5%      