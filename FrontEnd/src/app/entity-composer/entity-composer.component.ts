import {Component, OnInit} from '@angular/core'
import {DataProviderService} from '../services/data-provider.service'
import {PagingData, ColumnData} from '../model/models'
import {FormComponent} from "../form-elements/form.component";

@Component({
  moduleId: module.id,
  selector: 'app-entity-composer',
  templateUrl: 'entity-composer.component.html',
  styleUrls: ['entity-composer.component.css'],
  directives: [FormComponent]
})
export class EntityComposerComponent implements OnInit {

  entity = "user"

  /// filters that user set
  filters = {}

  /// current page
  page = 0

  /// item perpage
  itemsPerPage = 10

  /// order by
  orderBy: string

  /// is sorting acsding
  isAscending = true

  /// columns data
  columnsData: ColumnData[]

  /// data to display
  data: any[]

  /// is loading
  isLoading = false

  /// constructers
  constructor(private provider: DataProviderService) {
  }

  /**
   * Initialize the project
   */
  ngOnInit() {
    this.loadData()
  }


  /**
   * load data
   */
  loadData() {
    /*this.isLoading = true

     this.provider.queryData(
     this.entity,
     this.filters,
     this.page * this.itemsPerPage,
     this.itemsPerPage,
     this.orderBy,
     this.isAscending,
     this.columnsData === undefined
     ).subscribe(
     data => this.dataLoaded(data)
     )*/
  }


  /**
   * handle data loaded
   */
  dataLoaded(pagingData: PagingData<any>) {
    if (pagingData.metaData) {
      this.setMetaData(pagingData.metaData)
    }

    this.data = pagingData.data
    this.isLoading = false
  }

  /**
   * set the metdata
   * @param metaData
   */
  setMetaData(metaData: ColumnData[]) {
    this.columnsData = metaData.filter(
      item => item.displayName != null
    )
  }
}
