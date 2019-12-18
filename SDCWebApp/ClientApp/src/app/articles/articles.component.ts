import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { Article } from '../models/Article';
import { ArticleService } from 'src/app/services/article.service';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-articles',
    templateUrl: './articles.component.html',
    styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent implements OnInit {
    public articles = new Subject<Article[]>();
    private readonly title = 'News';

    constructor(private articleService: ArticleService, private titleService: Title) {
        this.titleService.setTitle(this.title);
    }

    ngOnInit() {
        this.articleService.getAllArticles().subscribe(articles => {
            this.articles.next(articles.sort(this.sortChronological));
        });
    }

    private sortChronological(a: Article, b: Article) {
        const aUpdated = new Date(a.updatedAt);
        const bUpdated = new Date(b.updatedAt);
        const aCreated = new Date(a.createdAt);
        const bCreated = new Date(b.createdAt);

        if (aUpdated !== null) {
            if (bUpdated !== null) {
                return aUpdated > bUpdated ? -1 : 1;
            } else {
                return aUpdated > bCreated ? -1 : 1;
            }
        } else {
            if (bUpdated !== null) {
                return aCreated > bUpdated ? -1 : 1;
            } else {
                return aCreated > bCreated ? -1 : 1;
            }
        }
    }
}
