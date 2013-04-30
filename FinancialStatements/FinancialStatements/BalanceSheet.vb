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

   
    Private Sub SaveToName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToName.Click

        'SaveFileDialogクラスのインスタンスの作成
        Dim sfd As New SaveFileDialog

        '始めのファイル名を指定する
        sfd.FileName = "新しいファイル.csv"
        '始めに表示されるフォルダを指定する。
        sfd.InitialDirectory = Application.StartupPath
        '[ファイルの種類]に表示される選択しを指定する。
        sfd.Filter = "CSVファイル(*.csv)|*.csv"
        'タイトルを設定する
        sfd.Title = "保存する先のファイルを選択してください。"
        'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする。
        sfd.RestoreDirectory = True

        'ダイアログを表示する。
        If sfd.ShowDialog() = DialogResult.OK Then
            'OKボタンがクリックされたとき

            '選択されたファイル名を表示する sfd.FileName
            'Shift-JISで保存します。
            Dim swText As New System.IO.StreamWriter(sfd.FileName, _
                                                     False, System.Text.Encoding.GetEncoding("Shift-JIS"))
            swText.Write(GetTextForSaveToCsv(LiquidGridView))
            swText.Dispose()

        End If
    End Sub

    '''<summary>保存したいDataGridViewコントロールの名前を引数として設定</summary>
    Public Function GetTextForSaveToCsv(ByVal tempDgv As DataGridView) As String

        '１行もデータがない場合は、保存を中止します。
        If tempDgv.Rows.Count = 0 Then
            Exit Function
        End If

        '変数を定義します。
        Dim i As Integer
        Dim j As Integer
        Dim strResult As New System.Text.StringBuilder

        '保存ダイアログでファイル名を設定した場合に処理を実行します。
        'コラムヘッダを１行目に列記します。
        '※ヘッダ行が不要な場合は削除可能です。
        For i = 0 To tempDgv.Columns.Count - 1
            Select Case i
                Case 0
                    '最初の "text" みたいな感じ
                    strResult.Append("""" & tempDgv.Columns(i).HeaderText.ToString & """")

                Case tempDgv.Columns.Count - 1
                    strResult.Append("," & """" & tempDgv.Columns(i).HeaderText.ToString & """" & vbCrLf)

                Case Else
                    strResult.Append("," & """" & tempDgv.Columns(i).HeaderText.ToString & """")
            End Select
        Next

        'データを保存します
        '※新規行の追加を認めている場合は、次行の「tempDgv.Columns.Count - 1」を
        '「tempDgv.Columns.Count - 2」としてください。
        For i = 0 To tempDgv.Rows.Count - 1
            For j = 0 To tempDgv.Columns.Count - 1
                Select Case j
                    Case 0
                        strResult.Append("""" & tempDgv.Rows(i).Cells(j).Value.ToString & """")

                    Case tempDgv.Columns.Count - 1
                        strResult.Append("," & """" & tempDgv.Rows(i).Cells(j).Value.ToString & """" & vbCrLf)

                    Case Else
                        strResult.Append("," & """" & tempDgv.Rows(i).Cells(j).Value.ToString & """")
                End Select
            Next
        Next
        Return strResult.ToString
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        LiquidGridView.Rows.Add()
        LiquidGridView.Rows(LiquidGridView.RowCount - 1).Cells(0).Value = "hoge"
    End Sub
End Class
