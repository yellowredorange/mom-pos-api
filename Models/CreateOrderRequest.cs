using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MomPosApi.Models {
    public class CreateOrderRequest {
        public required List<CreateOrderItemDto> Items { get; set; }
    }

}
