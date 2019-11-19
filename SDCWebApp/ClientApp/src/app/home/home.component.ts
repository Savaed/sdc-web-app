import { Component, OnInit } from '@angular/core';
import { VisitInfoService } from 'src/app/services/visit-info.service';
import { Subject, BehaviorSubject } from 'rxjs';
import { Article } from '../models/Article';
import { ArticleService } from 'src/app/services/article.service';
import { VisitInfo } from '../models/VisitInfo';
import { TicketTariffService } from 'src/app/services/ticket-tariff.service';
import { TicketTariffJson, TicketTariff } from '../models/TicketTariff';
import { Title } from '@angular/platform-browser';
import { ToastService } from '../services/toast.service';


@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
    private topThreeArticles = new Subject<Article[]>();
    private recentVisitInfo = new Subject<VisitInfo>();
    private isOpenNow = new Subject<boolean>();
    private now: Date;
    private readonly title = 'Glass Heritage Centrum';

    constructor(private infoService: VisitInfoService, private articleService: ArticleService, private titleService: Title, private toast: ToastService) {
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
        this.articleService.getAllArticles().subscribe(articles => this.topThreeArticles.next(articles.slice(0, 3)));      
    }
}
