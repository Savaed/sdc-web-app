import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { Article } from '../models/Article';
import { ArticleService } from 'src/app/services/article.service';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-articles',
    templateUrl: './articles.component.html',
    styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent implements OnInit {
    private articles = new Subject<Article[]>();
    private readonly title = 'News';

    constructor(private articleService: ArticleService, private router: Router, private titleService: Title) {
        this.titleService.setTitle(this.title);
    }

    ngOnInit() {
        this.articleService.getAllArticles().subscribe(articles => this.articles.next(articles));
    }
}
