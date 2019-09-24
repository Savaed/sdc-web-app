import { Component, OnInit } from '@angular/core';
import { VisitInfoService } from '../services/visit-info.service';

@Component({
    selector: 'app-visit-sdc',
    templateUrl: './visit-sdc.component.html',
    styleUrls: ['./visit-sdc.component.scss']
})
export class VisitSdcComponent implements OnInit {

    constructor(private visitInfoService: VisitInfoService) { }

    ngOnInit() {
        this.visitInfoService.getRecentInfo().subscribe(response => console.log(response.data.description));
    }

}
