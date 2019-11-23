import { Component, OnInit, Input } from '@angular/core';
import { Article } from 'src/app/models/Article';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ResourceService, ResourceType } from '../../../resource.service';

@Component({
    selector: 'app-article-form',
    templateUrl: './article-form.component.html',
    styleUrls: ['./article-form.component.scss']
})
export class ArticleFormComponent implements OnInit {
    private articleForm: FormGroup;

    @Input() public article: Article = { author: '', text: '', title: '' };
    @Input() public isForAdd = false;

    private get title() { return this.articleForm.get('title') as FormControl; }
    private get text() { return this.articleForm.get('text') as FormControl; }
    private get author() { return this.articleForm.get('author') as FormControl; }

    constructor(private formBuilder: FormBuilder, private resourceService: ResourceService) { }

    ngOnInit() {
        this.articleForm = this.formBuilder.group({
            title: [this.article.title, [Validators.maxLength(50), Validators.required]],
            text: [this.article.text, [Validators.maxLength(5000), Validators.required]],
            author: [this.article.author, [Validators.maxLength(50), Validators.required]]
        });
    }

    private edit() {
        const updatedArticle: Article = {
            id: this.article.id,
            createdAt: this.article.createdAt,
            updatedAt: this.article.updatedAt,
            author: this.author.value,
            text: this.text.value,
            title: this.title.value
        };

        if (this.isDifferent(this.article, updatedArticle)) {
            this.article = this.resourceService.edit(updatedArticle);
        }
    }

    private isDifferent(oldArticle: Article, newArticle: Article): boolean {
        return oldArticle.title !== newArticle.title || oldArticle.text !== newArticle.text || oldArticle.author !== newArticle.author;
    }

    private add() {
        const newArticle: Article = {
            text: this.text.value,
            author: this.author.value,
            title: this.title.value
        };

        this.resourceService.add(newArticle, ResourceType.Article);
    }

    private testCRUD() {
        if (this.isForAdd) {
            this.add();
        } else {
            this.edit();
        }
    }
}
