Public Class KarisyakuForm

    Private Sub KarisyakuForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ShowItems()

    End Sub


    ''' <summary>初期項目を表示させる</summary>
    Public Sub ShowItems()

        'Gridを増やす
        '初期項目を表示させる
        '流動資産の項目
        Dim NewItems As New Items

        For Each St As String In NewItems.LoadDefItems(AssetName.LiquidAsset)
            LiquidGridView.Rows.Add()
        Next

        Dim i As Integer = 0

        For Each St As String In NewItems.LoadDefItems(AssetName.LiquidAsset)
            LiquidGridView.Rows(i).Cells(0).Value = St
            i += 1
        Next

        '固定資産の項目
        For Each St As String In NewItems.LoadDefItems(AssetName.FixidAsset)
            FixedGridView.Rows.Add()
        Next
        '初期化
        i = 0

        For Each St As String In NewItems.LoadDefItems(AssetName.FixidAsset)
            FixedGridView.Rows(i).Cells(0).Value = St
            i += 1
        Next

        '負債の項目
        For Each St As String In NewItems.LoadDefItems(AssetName.Liability)
            LiabilityGridView.Rows.Add()
        Next
        '初期化
        i = 0

        For Each St As String In NewItems.LoadDefItems(AssetName.Liability)
            LiabilityGridView.Rows(i).Cells(0).Value = St
            i += 1
        Next
    End Sub

    ''' <summary>流動資産のEditingControlShowingイベントハンドラ
    ''' DataGridViewのTextBoxにフォーカスが移ったら実行される</summary>
    Private Sub LiquidGridView_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles LiquidGridView.EditingControlShowing

        '表示されているコントロールがDataGridViewTextBoxEditingControlか調べる
        If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then
            Dim dgv As DataGridView = CType(sender, DataGridView)

            '編集のために表示されているコントロールを取得
            Dim tb As DataGridViewTextBoxEditingControl = _
                CType(e.Control, DataGridViewTextBoxEditingControl)

            'イベントハンドラを削除
            RemoveHandler tb.KeyPress, AddressOf dataGridViewTextBox_KeyPress

            '該当する列か調べる
            If dgv.CurrentCell.OwningColumn.Name = "Money" Then
                'KeyPressイベントハンドラを追加
                AddHandler tb.KeyPress, AddressOf dataGridViewTextBox_KeyPress
            End If
        End If
    End Sub

    ''' <summary>DataGridViewに表示されているテキストボックスのKeyPressイベントハンドラ</summary>
    Private Sub dataGridViewTextBox_KeyPress(ByVal sender As Object, _
                                             ByVal e As KeyPressEventArgs)
        '数字しか入力できないようにする
        If e.KeyChar < "0"c Or e.KeyChar > "9"c Then
            e.Handled = True
        End If

    End Sub

    ''' <summary>金額項目からフォーカスが変わったときのイベントハンドラ</summary>
    Private Sub LiquidGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LiquidGridView.SelectionChanged

        Dim dgv As DataGridView = CType(sender, DataGridView)
        If dgv.CurrentCell.OwningColumn.Name = "Money" Then
            RyudouMoney_Sum.Text = GetSumMoney(AssetName.LiquidAsset)
            SetTotalMoney()
        End If

    End Sub

    ''' <summary>項目の値を足していくメソッド</summary>
    ''' <param name="ItemName">資産名列挙体</param>
    Private Function GetSumMoney(ByVal ItemName As AssetName) As Integer
        Dim Money As Integer = 0
        Select Case ItemName
            Case AssetName.LiquidAsset
                For i As Integer = 0 To LiquidGridView.Rows.Count - 1
                    'CInt(LiquidGridView.Rows(i).Cells(1).Value)
                    Try
                        Money += CInt(LiquidGridView.Rows(i).Cells(1).Value)
                    Catch ex As Exception
                        'なにもしない
                    End Try
                Next
                Return Money
            Case AssetName.FixidAsset
                For i As Integer = 0 To FixedGridView.Rows.Count - 1
                    Try
                        Money += CInt(FixedGridView.Rows(i).Cells(1).Value)
                    Catch ex As Exception
                        'なにもしない
                    End Try
                Next
                Return Money
            Case AssetName.Liability
                For i As Integer = 0 To LiabilityGridView.Rows.Count - 1
                    Try
                        Money += CInt(LiabilityGridView.Rows(i).Cells(1).Value)
                    Catch ex As Exception
                        'なにもしない
                    End Try
                Next
                Return Money
        End Select

    End Function

    ''' <summary>固定資産のEditingControlShowingイベントハンドラ
    ''' DataGridViewのTextBoxにフォーカスが移ったら実行される</summary>
    Private Sub FixedGridView_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles FixedGridView.EditingControlShowing

        '表示されているコントロールがDataGridViewTextBoxEditingControlか調べる
        If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then
            Dim dgv As DataGridView = CType(sender, DataGridView)

            '編集のために表示されているコントロールを取得
            Dim tb As DataGridViewTextBoxEditingControl = _
                CType(e.Control, DataGridViewTextBoxEditingControl)

            'イベントハンドラを削除
            RemoveHandler tb.KeyPress, AddressOf dataGridViewTextBox_KeyPress

            '該当する列か調べる
            If dgv.CurrentCell.OwningColumn.Name = "FixedMoney" Then
                'KeyPressイベントハンドラを追加
                AddHandler tb.KeyPress, AddressOf dataGridViewTextBox_KeyPress
            End If
        End If
    End Sub

    Private Sub FixedGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FixedGridView.SelectionChanged

        Dim dgv As DataGridView = CType(sender, DataGridView)
        If dgv.CurrentCell.OwningColumn.Name = "FixedMoney" Then
            Kotei_Sum.Text = GetSumMoney(AssetName.FixidAsset)
            SetTotalMoney()

        End If

    End Sub

    ''' <summary>負債のEditingControlShowingイベントハンドラ
    ''' DataGridViewのTextBoxにフォーカスが移ったら実行される</summary>

    Private Sub LiabilityGridView_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles LiabilityGridView.EditingControlShowing

        '表示されているコントロールがDataGridViewTextBoxEditingControlか調べる
        If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then
            Dim dgv As DataGridView = CType(sender, DataGridView)

            '編集のために表示されているコントロールを取得
            Dim tb As DataGridViewTextBoxEditingControl = _
                CType(e.Control, DataGridViewTextBoxEditingControl)

            'イベントハンドラを削除
            RemoveHandler tb.KeyPress, AddressOf dataGridViewTextBox_KeyPress

            '該当する列か調べる
            If dgv.CurrentCell.OwningColumn.Name = "LiabilityMoney" Then
                'KeyPressイベントハンドラを追加
                AddHandler tb.KeyPress, AddressOf dataGridViewTextBox_KeyPress
            End If
        End If
    End Sub


    Private Sub LiabilityGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LiabilityGridView.SelectionChanged

        Dim dgv As DataGridView = CType(sender, DataGridView)
        If dgv.CurrentCell.OwningColumn.Name = "LiabilityMoney" Then
            Husai_Sum.Text = GetSumMoney(AssetName.Liability)
            SetTotalMoney()

        End If

    End Sub

    '合計値の計算
    Private Sub SetTotalMoney()
        AssetTotal.Text = CInt(RyudouMoney_Sum.Text) + CInt(Kotei_Sum.Text)
        NetAssets.Text = CInt(AssetTotal.Text) - CInt(Husai_Sum.Text)
        TotalMoney.Text = CInt(Husai_Sum.Text) + CInt(NetAssets.Text)
    End Sub

   
End Class
