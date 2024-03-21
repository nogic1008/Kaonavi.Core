using Kaonavi.Net.Entities;

namespace Kaonavi.Net.Api;

/// <summary>
/// 所属ツリー API
/// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E6%89%80%E5%B1%9E%E3%83%84%E3%83%AA%E3%83%BC"/>
/// </summary>
public interface IDepartment
{
    /// <summary>
    /// <inheritdoc cref="DepartmentTree" path="/summary/text()"/>の情報を取得します。
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E6%89%80%E5%B1%9E%E3%83%84%E3%83%AA%E3%83%BC/paths/~1departments/get"/>
    /// </summary>
    /// <param name="cancellationToken">キャンセル通知を受け取るために他のオブジェクトまたはスレッドで使用できるキャンセル トークン。</param>
    ValueTask<IReadOnlyCollection<DepartmentTree>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// <inheritdoc cref="DepartmentTree" path="/summary/text()"/>を一括更新します。
    /// <paramref name="payload"/>に含まれていない情報は削除されます。
    /// <see href="https://developer.kaonavi.jp/api/v2.0/index.html#tag/%E6%89%80%E5%B1%9E%E3%83%84%E3%83%AA%E3%83%BC/paths/~1departments/put"/>
    /// </summary>
    /// <remarks>更新リクエスト制限の対象APIです。</remarks>
    /// <param name="payload">一括更新するデータ</param>
    /// <param name="cancellationToken">キャンセル通知を受け取るために他のオブジェクトまたはスレッドで使用できるキャンセル トークン。</param>
    /// <returns><inheritdoc cref="TaskProgress" path="/param[@name='Id']/text()"/></returns>
    ValueTask<int> ReplaceAsync(IReadOnlyCollection<DepartmentTree> payload, CancellationToken cancellationToken = default);
}
