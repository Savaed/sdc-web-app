import { Component, OnInit } from '@angular/core';
import { ResourceService, ResourceType } from '../resource.service';
import { AuthService } from 'src/app/services/auth.service';
import { BehaviorSubject } from 'rxjs';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
    public resourceType = ResourceType;
    private previousResourceType = this.resourceType.None;
    public showWelcomeBanner = new BehaviorSubject<boolean>(true);

    constructor(public resourceService: ResourceService, public authService: AuthService, public title: Title) { }

    ngOnInit() {
        this.title.setTitle('Dashboard');
        this.resourceService.setResourceList([]);
    }

    public showAddForm(resourceType: ResourceType) {
        switch (resourceType) {
            case ResourceType.Article:
                this.previousResourceType = this.resourceService.resourceType.getValue();
                this.resourceService.showAddArticleForm();
                break;

            case ResourceType.Discount:
                this.previousResourceType = this.resourceService.resourceType.getValue();
                this.resourceService.showAddDiscountForm();
                break;

            case ResourceType.VisitInfo:
                this.previousResourceType = this.resourceService.resourceType.getValue();
                this.resourceService.showAddVisitInfoForm();
                break;

            case ResourceType.TicketTariff:
                console.log('showAddForm() type: ', ResourceType[resourceType]);
                this.previousResourceType = this.resourceService.resourceType.getValue();
                this.resourceService.showTicketTariffForm();

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
            this.resourceService.setResourceType(this.previousResourceType);
        }
        this.hideAddForm();
    }
}
