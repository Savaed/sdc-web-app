import { Injectable } from '@angular/core';
import { ResourceType } from './resource.service';
import { Article } from 'src/app/models/Article';
import { Discount } from 'src/app/models/Discount';

@Injectable({
    providedIn: 'root'
})
export class ResourceSearchService {

    constructor() { }

    public filterResource(resourceType: ResourceType, resourceList: any[], question: string) {
        question = 'Black';
        console.log(question);


        if (question === '' || question === null || question === undefined) {
            return resourceList;
        }

        switch (resourceType) {
            case ResourceType.Article:
                return this.filterArticles(resourceList, question);
            default:
                break;
        }
    }

    private filterArticles(resourceList: Article[], question: string) {
        console.log('test search in search service');

        let returnList = new Array<any>();

        let tmpList: any[];
        tmpList = resourceList.filter(x => x.title.includes(question));

        if (tmpList.length > 0) {
            returnList = returnList.concat(tmpList);
        }
        tmpList = resourceList.filter(x => x.author.includes(question));
        if (tmpList.length > 0) {
            returnList = returnList.concat(tmpList);
        }
        tmpList = resourceList.filter(x => x.text.includes(question));
        if (tmpList.length > 0) {
            returnList = returnList.concat(tmpList);
        }

        console.log(returnList);

        // TODO: Remove duplicates.

        return returnList;
    }
}
