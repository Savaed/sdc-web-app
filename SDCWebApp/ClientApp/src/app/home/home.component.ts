import { Component, OnInit } from '@angular/core';
import { VisitInfoService } from 'src/app/services/visit-info.service';
import { Subject } from 'rxjs';
import { Article } from '../models/Article';
import { ArticleService } from 'src/app/services/article.service';
import { VisitInfo } from '../models/VisitInfo';
import { Title } from '@angular/platform-browser';
import { ToastrService } from 'ngx-toastr';


@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
    private readonly title = 'SDC';
    public topThreeArticles = new Subject<Article[]>();
    public recentVisitInfo = new Subject<VisitInfo>();
    public isOpenNow = new Subject<boolean>();
    public now: Date;

    constructor(private infoService: VisitInfoService, private articleService: ArticleService, private titleService: Title, private toastr: ToastrService) {
        this.titleService.setTitle(this.title);
    }

    ngOnInit() {
        this.infoService.getRecentInfo().subscribe(info => {
            this.recentVisitInfo.next(info);
            this.recentVisitInfo.subscribe(x => console.log(x));
            this.now = new Date();
            const openingHours = info.openingHours.find(hour => hour.dayOfWeek.toString() === this.now.dayToString());
            this.isOpenNow.next(this.now.getHours() >= parseInt(openingHours.openingHour.toString()) && this.now.getHours() <= parseInt(openingHours.closingHour.toString()));
        });
        this.articleService.getAllArticles().subscribe(articles => {
            articles.sort(this.sortChronological);
            this.topThreeArticles.next(articles.slice(0, 3));
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
}
