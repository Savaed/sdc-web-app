import { Component, OnInit } from '@angular/core';
import { ResourceService, ResourceType } from '../resource.service';
import { AuthService } from 'src/app/services/auth.service';
import { BehaviorSubject } from 'rxjs';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
    public resourceType = ResourceType;
    public showWelcomeBanner = new BehaviorSubject<boolean>(true);

    constructor(public resourceService: ResourceService, public authService: AuthService, public title: Title) { }

    ngOnInit() {
        this.title.setTitle('Dashboard');
    }

    public showAddForm(resourceType: ResourceType) {
        switch (resourceType) {
            case ResourceType.Article:
                this.resourceService.showAddArticleForm();
                break;

            case ResourceType.Discount:
                this.resourceService.showAddDiscountForm();
                break;

            case ResourceType.VisitInfo:
                this.resourceService.showAddVisitInfoForm();
                break;

            case ResourceType.TicketTariff:
                this.resourceService.showTicketTariffs();
                break;
        }

        this.resourceService.setAddMode(true);
    }

    public hideWelcomeBanner() { this.showWelcomeBanner.next(false); }

    public hideAddForm() {
        if (this.resourceService.isAddModeEnable.getValue() === true) {
            this.resourceService.setAddMode(false);
        }
    }

    public backToOverview() {
        if (this.resourceService.isEditModeEnable.getValue()) {
            this.resourceService.setEditMode(-1, false);
        } else {
            this.hideAddForm();
        }
    }
}
