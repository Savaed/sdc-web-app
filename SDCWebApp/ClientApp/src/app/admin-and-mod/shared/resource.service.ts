
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ArticleService } from 'src/app/services/article.service';
import { TicketService } from 'src/app/services/ticket.service';
import { DiscountService } from 'src/app/services/discount.service';
import { ActivityLogService } from 'src/app/services/activity-log.service';
import { TicketTariffService } from 'src/app/services/ticket-tariff.service';
import { CustomerService } from 'src/app/services/customer.service';
import { VisitGroupService } from 'src/app/services/visit-group.service';
import { VisitInfoService } from 'src/app/services/visit-info.service';
import { BaseResponseDataType } from 'src/app/models/BaseResponseDataType';
import { Article } from 'src/app/models/Article';
import { Discount } from 'src/app/models/Discount';
import { VisitInfo } from 'src/app/models/VisitInfo';
import { TicketTariff } from 'src/app/models/TicketTariff';
import { VisitTariff } from 'src/app/models/VisitTariff';
import { ReturnStatement } from '@angular/compiler';
import { ResourceSearchService } from './resource-search.service';

export enum ResourceType {
    None,
    Article,
    VisitInfo,
    VisitGroup,
    TicketTariff,
    VisitTariff,
    Customer,
    Discount,
    Ticket,
    Log
}

export enum SortableData {
    Id,
    CreatedAt,
    UpdatedAt
}

export enum SortOrder {
    Asc,
    Desc
}

@Injectable({
    providedIn: 'root'
})
export class ResourceService {
    private currentResourceList = new BehaviorSubject<any[]>(undefined);
    private currentResourceType = new BehaviorSubject<ResourceType>(ResourceType.None);
    private isEditMode = new BehaviorSubject<boolean>(false);
    private index = new BehaviorSubject<number>(-1);
    private addMode = new BehaviorSubject<boolean>(false);

    public get resourceList(): BehaviorSubject<any[]> { return this.currentResourceList; }
    public get resourceType(): BehaviorSubject<ResourceType> { return this.currentResourceType; }
    public get isEditModeEnable(): BehaviorSubject<boolean> { return this.isEditMode; }
    public get isAddModeEnable(): BehaviorSubject<boolean> { return this.addMode; }
    public get editIndex(): BehaviorSubject<number> { return this.index; }


    constructor(private articleService: ArticleService,
        private ticketService: TicketService,
        private discountService: DiscountService,
        private logService: ActivityLogService,
        private ticketTariffService: TicketTariffService,
        private customerService: CustomerService,
        private visitGroupService: VisitGroupService,
        private infoService: VisitInfoService,
        // private searchService: ResourceSearchService

    ) { }

    // public search(question: string) {
    //     console.log('search in resource service');

    //     question = '';
    //     const filteredResourceList = this.searchService.filterResource(this.resourceType.getValue(), this.resourceList.getValue(), question);
    //     this.resourceList.next(filteredResourceList);
    // }

    public setEditMode(index: number, value: boolean) {
        this.isEditMode.next(value);
        this.index.next(index);
    }

    public setAddMode(value: boolean) {
        this.addMode.next(value);
    }

    public showArticles() {
        this.articleService.getAllArticles().subscribe(a => {
            this.currentResourceList.next(a);
            this.currentResourceType.next(ResourceType.Article);
            this.isEditMode.next(false);
        });
    }

    public showTickets() {
        this.ticketService.getAllTickets().subscribe(t => {
            this.currentResourceList.next(t);
            this.currentResourceType.next(ResourceType.Ticket);
        });
    }

    public showDiscounts() {
        this.discountService.getAllDiscounts().subscribe(d => {
            this.currentResourceList.next(d);
            this.currentResourceType.next(ResourceType.Discount);
        });
    }

    public showTicketTariffs() {
        this.ticketTariffService.getAllTicketTariffs().subscribe(tt => {
            this.currentResourceList.next(tt);
            this.resourceType.next(ResourceType.TicketTariff);
        });
    }

    public showLogs() {
        this.logService.getAllLogs().subscribe(l => {
            this.currentResourceList.next(l);
            this.currentResourceType.next(ResourceType.Log);
        });
    }

    public showCustomers() {
        this.customerService.getAllCustomer().subscribe(c => {
            this.currentResourceList.next(c);
            this.currentResourceType.next(ResourceType.Customer);
        });
    }

    public showVisitGroups() {
        this.visitGroupService.getAllVisitGroups().subscribe(g => {
            this.currentResourceList.next(g);
            this.currentResourceType.next(ResourceType.VisitGroup);
        });
    }

    public showVisitInfo() {
        this.infoService.getAllInfo().subscribe(i => {
            this.currentResourceList.next(i);
            this.currentResourceType.next(ResourceType.VisitInfo);
        });
    }

    public showAddArticleForm() {
        this.resourceType.next(ResourceType.Article);
    }

    public showAddDiscountForm() {
        this.resourceType.next(ResourceType.Discount);
    }

    public showAddVisitInfoForm() {
        this.resourceType.next(ResourceType.VisitInfo);
    }

    public edit(resource: Article | Discount | VisitInfo | TicketTariff) {
        let updatedResource: any;

        switch (this.currentResourceType.getValue()) {
            case ResourceType.Article:
                this.articleService.updateArticle(resource.id, resource as Article).subscribe(a => {
                    updatedResource = a;
                    this.showArticles();
                });
                this.isEditMode.next(false);
                break;

            case ResourceType.Discount:
                this.discountService.updateDiscount(resource.id, resource as Discount).subscribe(d => {
                    updatedResource = d;
                    this.showDiscounts();
                });
                this.isEditMode.next(false);
                break;

            case ResourceType.VisitInfo:
                this.infoService.updateInfo(resource.id, resource as VisitInfo).subscribe(d => {
                    updatedResource = d;
                    this.showVisitInfo();
                });
                this.isEditMode.next(false);
                break;

            case ResourceType.TicketTariff:
                this.ticketTariffService.updateTicketTariff((resource as TicketTariff).visitTariffId, resource.id, resource as TicketTariff).subscribe(tt => {
                    updatedResource = tt;
                    this.showTicketTariffs();
                });
                this.isEditMode.next(false);
                break;

        }
        return updatedResource;
    }

    public add(resource: Article | Discount | VisitInfo | TicketTariff | VisitTariff, resourceType: ResourceType): any {
        let newResource: any;

        switch (resourceType) {
            case ResourceType.Article:
                this.articleService.addArticle(resource as Article).subscribe(a => {
                    newResource = a;
                    this.showArticles();
                });
                break;

            case ResourceType.Discount:
                this.discountService.addDiscount(resource as Discount).subscribe(a => {
                    newResource = a;
                    this.showDiscounts();
                });
                break;

            case ResourceType.VisitInfo:
                this.infoService.addInfo(resource as VisitInfo).subscribe(i => {
                    newResource = i;
                    this.showVisitInfo();
                });
                break;

            case ResourceType.TicketTariff:
                this.ticketTariffService.addTicketTariff((resource as TicketTariff).visitTariffId, resource as TicketTariff).subscribe(tt => {
                    newResource = tt;
                    this.showTicketTariffs();
                });
                break;
        }

        this.addMode.next(false);

        return newResource;
    }

    public delete(resource: Article | Discount | VisitInfo | TicketTariff) {
        let tmpList = new Array<any>();

        switch (this.currentResourceType.getValue()) {
            case ResourceType.Article:
                this.articleService.deleteArticle(resource.id).subscribe();
                tmpList = this.currentResourceList.getValue() as Article[];
                tmpList.splice(tmpList.indexOf(resource as Article), 1);
                this.currentResourceList.next(tmpList);
                this.isEditMode.next(false);
                break;

            case ResourceType.Discount:
                this.discountService.deleteDiscount(resource.id).subscribe();
                tmpList = this.currentResourceList.getValue() as Discount[];
                tmpList.splice(tmpList.indexOf(resource as Discount), 1);
                this.currentResourceList.next(tmpList);
                this.isEditMode.next(false);
                break;

            case ResourceType.VisitInfo:
                this.infoService.deleteInfo(resource.id).subscribe();
                tmpList = this.currentResourceList.getValue() as VisitInfo[];
                tmpList.splice(tmpList.indexOf(resource as VisitInfo), 1);
                this.currentResourceList.next(tmpList);
                this.isEditMode.next(false);
                break;

            case ResourceType.TicketTariff:
                this.ticketTariffService.deleteTicketTariff('1', resource.id).subscribe();
                tmpList = this.currentResourceList.getValue() as TicketTariff[];
                tmpList.splice(tmpList.indexOf(resource as TicketTariff), 1);
                this.currentResourceList.next(tmpList);
                this.isEditMode.next(false);
                break;
        }
    }
}
