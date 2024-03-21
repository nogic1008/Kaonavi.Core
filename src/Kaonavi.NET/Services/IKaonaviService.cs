using Kaonavi.Net.Entities;

namespace Kaonavi.Net.Services;

/// <summary>カオナビ API v2の抽象化</summary>
public interface IKaonaviService
{
    #region タスク進捗状況
    /// <summary>
    /// <paramref name="taskId"/>と一致する<inheritdoc cref="TaskProgress" path="/summary/text()"/>を取得します。
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%82%BF%E3%82%B9%E3%82%AF%E9%80%B2%E6%8D%97%E7%8A%B6%E6%B3%81/paths/~1tasks~1{task_id}/get"/>
    /// </summary>
    /// <param name="taskId"><inheritdoc cref="TaskProgress" path="/param[@name='Id']/text()"/></param>
    /// <param name="cancellationToken">キャンセル通知を受け取るために他のオブジェクトまたはスレッドで使用できるキャンセル トークン。</param>
    ValueTask<TaskProgress> FetchTaskProgressAsync(int taskId, CancellationToken cancellationToken = default);
    #endregion タスク進捗状況

    #region レイアウト設定
    /// <summary>
    /// 使用可能なメンバーのレイアウト設定情報を全て取得します。
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%83%AC%E3%82%A4%E3%82%A2%E3%82%A6%E3%83%88%E8%A8%AD%E5%AE%9A/paths/~1member_layouts/get"/>
    /// </summary>
    /// <param name="cancellationToken"><inheritdoc cref="FetchTaskProgressAsync" path="/param[@name='cancellationToken']/text()"/></param>
    ValueTask<MemberLayout> FetchMemberLayoutAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 使用可能なシートのレイアウト設定情報を全て取得します。
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%83%AC%E3%82%A4%E3%82%A2%E3%82%A6%E3%83%88%E8%A8%AD%E5%AE%9A/paths/~1sheet_layouts/get"/>
    /// </summary>
    /// <param name="cancellationToken"><inheritdoc cref="FetchTaskProgressAsync" path="/param[@name='cancellationToken']/text()"/></param>
    ValueTask<IReadOnlyCollection<SheetLayout>> FetchSheetLayoutsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// <paramref name="sheetId"/>と一致するシートの使用可能なレイアウト設定を全て取得します。
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%83%AC%E3%82%A4%E3%82%A2%E3%82%A6%E3%83%88%E8%A8%AD%E5%AE%9A/paths/~1sheet_layouts~1{sheet_id}/get"/>
    /// </summary>
    /// <param name="sheetId"><inheritdoc cref="SheetLayout" path="/param[@name='Id']"/></param>
    /// <param name="cancellationToken"><inheritdoc cref="FetchTaskProgressAsync" path="/param[@name='cancellationToken']/text()"/></param>
    ValueTask<SheetLayout> FetchSheetLayoutAsync(int sheetId, CancellationToken cancellationToken = default);
    #endregion レイアウト設定

    #region メンバー情報
    /// <summary>
    /// 全てのメンバーの基本情報・所属(主務)・兼務情報を取得します。
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%83%A1%E3%83%B3%E3%83%90%E3%83%BC%E6%83%85%E5%A0%B1/paths/~1members/get"/>
    /// </summary>
    /// <param name="cancellationToken"><inheritdoc cref="FetchTaskProgressAsync" path="/param[@name='cancellationToken']/text()"/></param>
    ValueTask<IReadOnlyCollection<MemberData>> FetchMembersDataAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// メンバー登録と、合わせて基本情報・所属(主務)・兼務情報を登録します。
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%83%A1%E3%83%B3%E3%83%90%E3%83%BC%E6%83%85%E5%A0%B1/paths/~1members/post"/>
    /// </summary>
    /// <remarks>更新リクエスト制限の対象APIです。</remarks>
    /// <param name="payload">追加するデータ</param>
    /// <param name="cancellationToken"><inheritdoc cref="FetchTaskProgressAsync" path="/param[@name='cancellationToken']/text()"/></param>
    /// <returns><inheritdoc cref="TaskProgress" path="/param[@name='Id']/text()"/></returns>
    ValueTask<int> AddMemberDataAsync(IReadOnlyCollection<MemberData> payload, CancellationToken cancellationToken = default);

    /// <summary>
    /// 全てのメンバーの基本情報・所属(主務)・兼務情報を一括更新します。
    /// <paramref name="payload"/>に含まれていない情報は削除されます。
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%83%A1%E3%83%B3%E3%83%90%E3%83%BC%E6%83%85%E5%A0%B1/paths/~1members/put"/>
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>メンバーの登録・削除は行われません。</item>
    /// <item>更新リクエスト制限の対象APIです。</item>
    /// </list>
    /// </remarks>
    /// <param name="payload">一括更新するデータ</param>
    /// <param name="cancellationToken"><inheritdoc cref="FetchTaskProgressAsync" path="/param[@name='cancellationToken']/text()"/></param>
    /// <returns><inheritdoc cref="TaskProgress" path="/param[@name='Id']/text()"/></returns>
    ValueTask<int> ReplaceMemberDataAsync(IReadOnlyCollection<MemberData> payload, CancellationToken cancellationToken = default);

    /// <summary>
    /// 送信されたメンバーの基本情報・所属(主務)・兼務情報のみを更新します。
    /// <paramref name="payload"/>に含まれていない情報は更新されません。
    /// 特定の値を削除する場合は、<c>""</c>を送信してください。
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%83%A1%E3%83%B3%E3%83%90%E3%83%BC%E6%83%85%E5%A0%B1/paths/~1members/patch"/>
    /// </summary>
    /// <remarks>更新リクエスト制限の対象APIです。</remarks>
    /// <param name="payload">更新するデータ</param>
    /// <param name="cancellationToken"><inheritdoc cref="FetchTaskProgressAsync" path="/param[@name='cancellationToken']/text()"/></param>
    /// <returns><inheritdoc cref="TaskProgress" path="/param[@name='Id']/text()"/></returns>
    ValueTask<int> UpdateMemberDataAsync(IReadOnlyCollection<MemberData> payload, CancellationToken cancellationToken = default);

    /// <summary>
    /// 現在登録されているメンバーとそれに紐づく基本情報・所属(主務)・兼務情報を全て、<paramref name="payload"/>で入れ替えます。
    /// <list type="bullet">
    /// <item>存在しない社員番号を指定した場合、新しくメンバーを登録します。</item>
    /// <item>存在する社員番号を指定した場合、メンバー情報を更新します。</item>
    /// <item>存在する社員番号を指定していない場合、メンバーを削除します。</item>
    /// </list>
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%83%A1%E3%83%B3%E3%83%90%E3%83%BC%E6%83%85%E5%A0%B1/paths/~1members~1overwrite/put"/>
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>更新リクエスト制限の対象APIです。</item>
    /// <item>メンバーの削除はリクエスト時に登録処理が完了しているメンバーに対してのみ実行されます。</item>
    /// </list>
    /// </remarks>
    /// <param name="payload">入れ替え対象となるデータ</param>
    /// <param name="cancellationToken"><inheritdoc cref="FetchTaskProgressAsync" path="/param[@name='cancellationToken']/text()"/></param>
    /// <returns><inheritdoc cref="TaskProgress" path="/param[@name='Id']/text()"/></returns>
    ValueTask<int> OverWriteMemberDataAsync(IReadOnlyCollection<MemberData> payload, CancellationToken cancellationToken = default);

    /// <summary>
    /// 送信されたメンバーを削除します。
    /// 紐付けユーザーがいる場合、そのアカウント種別によってはユーザーも同時に削除されます。
    /// <list type="bullet">
    /// <item>一般の場合、ユーザーも同時に削除されます。</item>
    /// <item>Admの場合、ユーザーの紐付けが解除。引き続きそのユーザーでログインすることは可能です。</item>
    /// </list>
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%83%A1%E3%83%B3%E3%83%90%E3%83%BC%E6%83%85%E5%A0%B1/paths/~1members~1delete/post"/>
    /// </summary>
    /// <remarks>更新リクエスト制限の対象APIです。</remarks>
    /// <param name="codes">削除する<inheritdoc cref="MemberData" path="/param[@name='Code']/text()"/>のリスト</param>
    /// <param name="cancellationToken"><inheritdoc cref="FetchTaskProgressAsync" path="/param[@name='cancellationToken']/text()"/></param>
    /// <returns><inheritdoc cref="TaskProgress" path="/param[@name='Id']/text()"/></returns>
    ValueTask<int> DeleteMemberDataAsync(IReadOnlyCollection<string> codes, CancellationToken cancellationToken = default);
    #endregion メンバー情報

    #region シート情報
    /// <summary>
    /// <paramref name="sheetId"/>と一致するシートの全情報を取得します。
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%82%B7%E3%83%BC%E3%83%88%E6%83%85%E5%A0%B1/paths/~1sheets~1{sheet_id}/get"/>
    /// </summary>
    /// <param name="sheetId"><inheritdoc cref="SheetLayout" path="/param[@name='Id']/text()"/></param>
    /// <param name="cancellationToken"><inheritdoc cref="FetchTaskProgressAsync" path="/param[@name='cancellationToken']/text()"/></param>
    ValueTask<IReadOnlyCollection<SheetData>> FetchSheetDataListAsync(int sheetId, CancellationToken cancellationToken = default);

    /// <summary>
    /// <paramref name="sheetId"/>と一致する<inheritdoc cref="SheetData" path="/summary/text()"/>を一括更新します。
    /// <paramref name="payload"/>に含まれていない情報は削除されます。
    /// 複数レコードの情報は送信された配列順に登録されます。
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%82%B7%E3%83%BC%E3%83%88%E6%83%85%E5%A0%B1/paths/~1sheets~1{sheet_id}/put"/>
    /// </summary>
    /// <remarks>更新リクエスト制限の対象APIです。</remarks>
    /// <param name="sheetId"><inheritdoc cref="SheetLayout" path="/param[@name='Id']/text()"/></param>
    /// <param name="payload">一括更新するデータ</param>
    /// <param name="cancellationToken"><inheritdoc cref="FetchTaskProgressAsync" path="/param[@name='cancellationToken']/text()"/></param>
    /// <returns><inheritdoc cref="TaskProgress" path="/param[@name='Id']/text()"/></returns>
    ValueTask<int> ReplaceSheetDataAsync(int sheetId, IReadOnlyCollection<SheetData> payload, CancellationToken cancellationToken = default);

    /// <summary>
    /// <paramref name="sheetId"/>と一致する<inheritdoc cref="SheetData" path="/summary/text()"/>の一部を更新します。
    /// <list type="bullet">
    /// <item>
    ///   <term><inheritdoc cref="RecordType.Single" path="/summary/text()"/></term>
    ///   <description>
    ///     送信された情報のみが更新されます。
    ///     <paramref name="payload"/> に含まれていない情報は更新されません。
    ///     特定の値を削除する場合は、<c>""</c>を送信してください。
    ///   </description>
    /// </item>
    /// <item>
    ///   <term><inheritdoc cref="RecordType.Multiple" path="/summary/text()"/></term>
    ///   <description>
    ///     メンバーごとのデータが一括更新されます。
    ///     特定の値を削除する場合は、<c>""</c>を送信してください。
    ///     特定のレコードだけを更新することは出来ません。
    ///     <inheritdoc cref="SheetData.Code" path="/summary/text()"/>が指定されていないメンバーの情報は更新されません。
    ///     送信された配列順に登録されます。
    ///   </description>
    /// </item>
    /// </list>
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%82%B7%E3%83%BC%E3%83%88%E6%83%85%E5%A0%B1/paths/~1sheets~1{sheet_id}/patch"/>
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>更新リクエスト制限の対象APIです。</item>
    /// </list>
    /// </remarks>
    /// <param name="sheetId"><inheritdoc cref="SheetLayout" path="/param[@name='Id']/text()"/></param>
    /// <param name="payload">更新するデータ</param>
    /// <param name="cancellationToken"><inheritdoc cref="FetchTaskProgressAsync" path="/param[@name='cancellationToken']/text()"/></param>
    /// <returns><inheritdoc cref="TaskProgress" path="/param[@name='Id']/text()"/></returns>
    ValueTask<int> UpdateSheetDataAsync(int sheetId, IReadOnlyCollection<SheetData> payload, CancellationToken cancellationToken = default);

    /// <summary>
    /// <paramref name="sheetId"/>と一致する<inheritdoc cref="RecordType.Multiple"/>にレコードを追加します。
    /// <inheritdoc cref="RecordType.Single"/>は対象外です。
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E3%82%B7%E3%83%BC%E3%83%88%E6%83%85%E5%A0%B1/paths/~1sheets~1{sheet_id}~1add/post"/>
    /// </summary>
    /// <param name="sheetId"><inheritdoc cref="SheetLayout" path="/param[@name='Id']/text()"/></param>
    /// <param name="payload">追加するデータ</param>
    /// <param name="cancellationToken"><inheritdoc cref="FetchTaskProgressAsync" path="/param[@name='cancellationToken']/text()"/></param>
    /// <returns><inheritdoc cref="TaskProgress" path="/param[@name='Id']/text()"/></returns>
    ValueTask<int> AddSheetDataAsync(int sheetId, IReadOnlyCollection<SheetData> payload, CancellationToken cancellationToken = default);
    #endregion シート情報
}
