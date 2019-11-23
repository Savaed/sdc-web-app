import { Component, OnInit, Input } from '@angular/core';
import { Article } from 'src/app/models/Article';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ResourceService, ResourceType } from '../../../resource.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-article-form',
    templateUrl: './article-form.component.html',
    styleUrls: ['./article-form.component.scss']
})
export class ArticleFormComponent implements OnInit {
    public articleForm: FormGroup;

    @Input() public article: Article = { author: '', text: '', title: '' };
    @Input() public isForAdd = false;

    public get title() { return this.articleForm.get('title') as FormControl; }
    public get text() { return this.articleForm.get('text') as FormControl; }
    public get author() { return this.articleForm.get('author') as FormControl; }

    constructor(private formBuilder: FormBuilder, private resourceService: ResourceService, private toast: ToastrService) { }

    ngOnInit() {
        this.articleForm = this.formBuilder.group({
            title: [this.article.title, [Validators.maxLength(50), Validators.required]],
            text: [this.article.text, [Validators.maxLength(5000), Validators.required]],
            author: [this.article.author, [Validators.maxLength(50), Validators.required]]
        });
    }

    public edit() {
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
            this.toast.info('Article has been edited.');
        }

    }

    public isDifferent(oldArticle: Article, newArticle: Article): boolean {
        return oldArticle.title !== newArticle.title || oldArticle.text !== newArticle.text || oldArticle.author !== newArticle.author;
    }

    public add() {
        const newArticle: Article = {
            text: this.text.value,
            author: this.author.value,
            title: this.title.value
        };

        this.resourceService.add(newArticle, ResourceType.Article);
        this.toast.info('New article has been added.');
    }

    public testCRUD() {
        if (this.isForAdd) {
            this.add();
        } else {
            this.edit();
        }
    }
}
