import { Component, OnInit, Input } from '@angular/core';
import { ResourceService } from '../resource.service';
import { Discount } from 'src/app/models/Discount';
import { Article } from 'src/app/models/Article';
import { TicketTariff } from 'src/app/models/TicketTariff';
import { VisitTariff } from 'src/app/models/VisitTariff';
import { VisitInfo } from 'src/app/models/VisitInfo';


@Component({
    selector: 'app-resource-overview',
    templateUrl: './resource-overview.component.html',
    styleUrls: ['./resource-overview.component.scss']
})
export class ResourceOverviewComponent implements OnInit {
    @Input() public id = '';
    @Input() public mainInfo: any = '';
    @Input() public secondaryInfo: any = '';
    @Input() public modifyDate: Date = new Date();
    @Input() public editable = false;

    constructor(private resourcService: ResourceService) { }

    ngOnInit() {
        // const datePattern = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(|Z)$/;
        // if (datePattern.test(this.mainInfo)) {
        //     this.mainInfo = (this.mainInfo as Date).toDateString();
        // }

        // if (datePattern.test(this.secondaryInfo)) {
        //     this.secondaryInfo = (this.secondaryInfo as Date).toDateString();
        // }
    }

    // private delete(resource: Article | Discount | VisitInfo | TicketTariff | VisitTariff) {
    //     this.resourcService.delete(resource);
    // }
}
