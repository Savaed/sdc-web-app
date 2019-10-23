import { Injectable } from '@angular/core';
import { Article } from '../models/Article';
import { ServerUrl } from '../helpers/Constants';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/ApiResponse';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class ArticleService {
    private readonly articleUrl = ServerUrl + '/articles';

    constructor(private http: HttpClient) { }

    public getArticle(id: string): Observable<Article> {
        return this.http.get<ApiResponse<Article>>(`${this.articleUrl}/${id}`).pipe(map(response => response.data));
    }

    public getAllArticles(): Observable<Article[]> {
        return this.http.get<ApiResponse<Article[]>>(this.articleUrl).pipe(map(response => response.data));
    }

    public addArticle(article: Article): Observable<Article> {
        return this.http.post<ApiResponse<Article>>(this.articleUrl, article).pipe(map(response => response.data));
    }

    public updateArticle(id: string, article: Article): Observable<Article> {
        return this.http.put<ApiResponse<Article>>(`${this.articleUrl}/${id}`, article).pipe(map(response => response.data));
    }

    public deleteArticle(id: string): Observable<ApiResponse<{}>> {
        return this.http.delete<ApiResponse<Article>>(`${this.articleUrl}/${id}`);
    }
}
