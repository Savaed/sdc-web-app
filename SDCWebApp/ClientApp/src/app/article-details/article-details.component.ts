import { Component, OnInit } from '@angular/core';
import { Article } from '../models/Article';
import { ArticleService } from '../services/article.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-article-details',
    templateUrl: './article-details.component.html',
    styleUrls: ['./article-details.component.scss']
})
export class ArticleDetailsComponent implements OnInit {
    private allArticles = new BehaviorSubject<Article[]>(undefined);
    private readArticle = new BehaviorSubject<Article>(undefined);
    private otherArticles = new BehaviorSubject<Article[]>(undefined);
    private nextArticle = new BehaviorSubject<Article>(undefined);

    constructor(private articleService: ArticleService, private activatedRoute: ActivatedRoute, private router: Router, private titleService: Title) {
        const articleName = this.activatedRoute.snapshot.params.articleName as string;
        this.titleService.setTitle('News - ' + articleName);
    }

    ngOnInit() {
        this.articleService.getAllArticles().subscribe(articles => {
            this.allArticles.next(articles);
            const articleName = this.activatedRoute.snapshot.params.articleName as string;
            console.log(articleName);
            this.readArticle.next(articles.find(x => x.title.toLowerCase() === articleName.toLowerCase()));
            this.otherArticles.next(articles.filter(x => x.title.toLowerCase() !== articleName.toLowerCase()).sort((a, b) => this.sortChronological(a, b)).slice(0, 8));
            this.nextArticle.next(this.otherArticles.getValue()[0]);
            console.log(this.otherArticles.getValue());
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
            if (b.updatedAt !== null) {
                return aCreated > bUpdated ? -1 : 1;
            } else {
                return aCreated > bCreated ? -1 : 1;
            }
        }
    }

    private navigateToArticle(articleTitle: string) {
        this.router.navigate(['news', articleTitle]);
        const allArticles = this.allArticles.getValue();
        this.readArticle.next(allArticles.find(x => x.title.toLowerCase() === articleTitle.toLowerCase()));
        this.otherArticles.next(allArticles.filter(x => x.title.toLowerCase() !== articleTitle.toLowerCase()).sort((a, b) => this.sortChronological(a, b)).slice(0, 8));
        this.nextArticle.next(this.otherArticles.getValue()[0]);

        this.titleService.setTitle('News - ' + articleTitle);
    }
}
